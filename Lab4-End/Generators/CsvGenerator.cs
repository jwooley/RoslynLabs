using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Linq;
using System.Text;

namespace Generators
{
    [Generator]
    public class CsvGenerator : ISourceGenerator
    {
        private const string csvSerializerAttributeText = @"
using System;
namespace CsvSerializer;

[AttributeUsage(AttributeTargets.Class)]
public class CsvSerializableAttribute : Attribute
{
    public CsvSerializableAttribute() {}
}";

        public void Execute(GeneratorExecutionContext context)
        {
            context.AddSource("CsvSerializableAttribute.g.cs", SourceText.From(csvSerializerAttributeText, Encoding.UTF8));

            if (!(context.SyntaxReceiver is CsvGeneratorSyntaxReceiver receiver))
            {
                return;
            }

            var options = (context.Compilation as CSharpCompilation).SyntaxTrees[0].Options as CSharpParseOptions;

            var attributeSyntaxTree = CSharpSyntaxTree.ParseText(SourceText.From(csvSerializerAttributeText, Encoding.UTF8), options);
            Compilation compilation = context.Compilation.AddSyntaxTrees(attributeSyntaxTree);

            INamedTypeSymbol stringSymbol = compilation.GetTypeByMetadataName("System.String");

            var sb = new StringBuilder();
            // Initialize class
            sb.Append(@"
using System.Linq;
namespace CsvSerializer;

public static class GeneratedSerializer
{");

            foreach (var classDeclaration in receiver.AttributeClasses)
            {
                if (classDeclaration == null)
                {
                    continue;
                }
                SemanticModel model = compilation.GetSemanticModel(classDeclaration.SyntaxTree);
                ITypeSymbol classSymbol = model.GetDeclaredSymbol(classDeclaration);

                var classFullName = !string.IsNullOrEmpty(classSymbol.ContainingNamespace?.Name) ?
                    $"{classSymbol.ContainingNamespace.Name}.{classDeclaration.Identifier.Text}"
                    : classDeclaration.Identifier.Text;

                sb.Append($@"
    #region {classFullName}
    public static string ToCsv(this {classFullName} input) =>
        $""");

                var header = new StringBuilder();
                var propertyCount = 0;
                foreach (var propertyDeclaration in classDeclaration.Members.OfType<PropertyDeclarationSyntax>())
                {
                    if (propertyCount > 0)
                    {
                        sb.Append(", ");
                        header.Append(", ");
                    }
                    IPropertySymbol propertySymbol = model.GetDeclaredSymbol(propertyDeclaration);
                    if (propertySymbol.Type.Equals(stringSymbol, SymbolEqualityComparer.Default))
                    {
                        sb.Append($"\\\"{{input.{propertyDeclaration.Identifier.Text}}}\\\"");
                    }
                    else
                    {
                        sb.Append($"{{input.{propertyDeclaration.Identifier.Text}}}");
                    }
                    header.Append(propertyDeclaration.Identifier.Text);
                    propertyCount++;
                }
                sb.Append(@""";
");

                sb.Append($@"
    public static string ToCsvHeader(this {classFullName} input) =>
        ""{header}"";
");
                sb.Append($@"
    public static string ToCsv(this System.Collections.Generic.IEnumerable<{classFullName}> input)
    {{
        var sb = new System.Text.StringBuilder();
        if (input.Any())
        {{
            sb.AppendLine(input.First().ToCsvHeader());
            foreach (var item in input)
            {{
                sb.AppendLine(item.ToCsv());
            }}
        }}
        return sb.ToString();
    }}

    #endregion
");
            }
            // Close class
            sb.Append(@"
}");
            context.AddSource("CsvSerializer.g.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new CsvGeneratorSyntaxReceiver());
        }
    }
}
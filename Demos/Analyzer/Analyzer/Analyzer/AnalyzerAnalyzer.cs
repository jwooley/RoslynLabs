using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

namespace Analyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AnalyzerAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "Demo001";

        // You can change these strings in the Resources.resx file. If you do not want your analyzer to be localize-able, you can use regular strings for Title and MessageFormat.
        // See https://github.com/dotnet/roslyn/blob/main/docs/analyzers/Localizing%20Analyzers.md for more on localization
        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Naming";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            // TODO: Consider registering other actions that act on syntax instead of or in addition to symbols
            // See https://github.com/dotnet/roslyn/blob/main/docs/analyzers/Analyzer%20Actions%20Semantics.md for more information

            context.RegisterSyntaxNodeAction(AnalyzeForeach, SyntaxKind.ForEachStatement);
            context.RegisterSyntaxNodeAction(AnalyzeLocal, SyntaxKind.LocalDeclarationStatement);

        }

        private static void AnalyzeForeach(SyntaxNodeAnalysisContext context)
        {
            // TODO: Replace the following code with your own analysis, generating Diagnostic objects for any issues you find
            if (context.Node is ForEachStatementSyntax syntax)
            {
                var identifier = syntax.Identifier;
                if (identifier.IsMissing)
                {
                    return;
                }
                if (identifier.ValueText.Length == 1)
                {
                    var name = identifier.ValueText;
                    context.ReportDiagnostic(Diagnostic.Create(Rule, identifier.GetLocation(), name));
                }

            }
        }

        private static void AnalyzeLocal(SyntaxNodeAnalysisContext context)
        {
            // TODO: Replace the following code with your own analysis, generating Diagnostic objects for any issues you find
            if (context.Node is LocalDeclarationStatementSyntax syntax)
            {
                var variables = syntax.Declaration?.Variables;
                if (variables is null)
                {
                    return;
                }
                foreach (var declarator in variables.Value)
                {
                    if (declarator is null)
                    {
                        continue;
                    }
                    var identifier = declarator.Identifier;
                    if (identifier.IsMissing)
                    {
                        continue;
                    }
                    if (identifier.ValueText.Length == 1)
                    {
                        var name = identifier.ValueText;
                        context.ReportDiagnostic(Diagnostic.Create(Rule, identifier.GetLocation(), name));
                    }
                }
            }
        }
    }
}

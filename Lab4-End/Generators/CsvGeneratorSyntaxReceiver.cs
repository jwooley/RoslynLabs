using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace Generators
{
    public class CsvGeneratorSyntaxReceiver : ISyntaxReceiver
    {
        public List<ClassDeclarationSyntax> AttributeClasses { get; } = new List<ClassDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax classSyntax)
            {
                foreach (var al in classSyntax.AttributeLists)
                {
                    foreach (var attribute in al.Attributes)
                    {
                        if (attribute.Name.GetText().ToString() == "CsvSerializable")
                        {
                            AttributeClasses.Add(classSyntax);
                        }
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.CodeAnalysis.Text;
using System.Text.RegularExpressions;

namespace Analyzer1
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(Analyzer1CodeFixProvider)), Shared]
    public class Analyzer1CodeFixProvider : CodeFixProvider
    {
        private const string title = "Ensure type ends in 'Controller'";

        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(Analyzer1Analyzer.DiagnosticId); }
        }

        public sealed override FixAllProvider GetFixAllProvider()
        {
            // See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/FixAllProvider.md for more information on Fix All Providers
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;
            var declaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<TypeDeclarationSyntax>().First();

            context.RegisterCodeFix(CodeAction.Create(title, c => MakeEndInControllerAsync(context.Document, declaration, c), nameof(Analyzer1CodeFixProvider)), diagnostic);
        }

        private async Task<Solution> MakeEndInControllerAsync(Document document, TypeDeclarationSyntax typeDecl, CancellationToken cancellationToken)
        {
            var identifierToken = typeDecl.Identifier;
            var originalName = identifierToken.Text;
            var nameWithoutController = Regex.Replace(originalName, "controller", String.Empty, RegexOptions.IgnoreCase);
            var newName = nameWithoutController + "Controller";

            var semanticModel = await document.GetSemanticModelAsync(cancellationToken);
            var typeSymbol = semanticModel.GetDeclaredSymbol(typeDecl, cancellationToken);
            //var newSymbol = SyntaxFactory.ClassDeclaration(newName)
            //    .WithMembers(typeDecl.Members)
            //    .WithLeadingTrivia(typeDecl.GetLeadingTrivia())
            //    .WithTrailingTrivia(typeDecl.GetTrailingTrivia())
            //    .WithAdditionalAnnotations(Formatter.Annotation);

            //var root = await document.GetSyntaxRootAsync(cancellationToken);
            //var newIdentifier = SyntaxFactory.Identifier(newName)
            //    .WithAdditionalAnnotations(Formatter.Annotation);
            //var newDeclaration = typeDecl.ReplaceToken(identifierToken, newIdentifier);
            //var newRoot = root.ReplaceNode(typeDecl, newDeclaration);
            //return document
            //    .WithSyntaxRoot(newRoot);

            var originalSolution = document.Project.Solution;
            var optionSet = originalSolution.Workspace.Options;

            var newSolution = await Renamer.RenameSymbolAsync(originalSolution, typeSymbol, newName, optionSet, cancellationToken).ConfigureAwait(false);

            return newSolution;
        }
    }
}
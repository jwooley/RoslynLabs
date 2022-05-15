using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;
using System;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Analyzer1
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(Analyzer1CodeFixProvider)), Shared]
    public class Analyzer1CodeFixProvider : CodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(Analyzer1Analyzer.DiagnosticId); }
        }

        public sealed override FixAllProvider GetFixAllProvider()
        {
            // See https://github.com/dotnet/roslyn/blob/main/docs/analyzers/FixAllProvider.md for more information on Fix All Providers
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            // TODO: Replace the following code with your own analysis, generating a CodeAction for each fix to suggest
            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;

            // Find the type declaration identified by the diagnostic.
            var declaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<TypeDeclarationSyntax>().First();

            // Register a code action that will invoke the fix.
            context.RegisterCodeFix(
                CodeAction.Create(
                    title: CodeFixResources.CodeFixTitle,
                    createChangedSolution: c => MakeEndInControllerAsync(context.Document, declaration, c),
                    equivalenceKey: nameof(CodeFixResources.CodeFixTitle)),
                diagnostic);
        }

        //private static async Task<Document> MakeEndInControllerAsync(
        //    Document document,
        //    TypeDeclarationSyntax typeDecl,
        //    CancellationToken cancellationToken)
        //{
        //    var identifierToken = typeDecl.Identifier;
        //    var originalName = identifierToken.Text;
        //    var nameWithoutController =
        //        Regex.Replace(originalName, "controller", String.Empty,
        //        RegexOptions.IgnoreCase);

        //    var newName = nameWithoutController + "Controller";
        //    var root = await document.GetSyntaxRootAsync(cancellationToken);

        //    var newIdentifier = SyntaxFactory.Identifier(newName)
        //        .WithAdditionalAnnotations(Formatter.Annotation);
        //    var newDeclaration =
        //        typeDecl.ReplaceToken(identifierToken, newIdentifier);
        //    var newRoot = root.ReplaceNode(typeDecl, newDeclaration);

        //    return document
        //        .WithSyntaxRoot(newRoot);
        //}

        private static async Task<Solution> MakeEndInControllerAsync(
            Document document,
            TypeDeclarationSyntax typeDecl,
            CancellationToken cancellationToken)
        {
            var identifierToken = typeDecl.Identifier;
            var originalName = identifierToken.Text;
            var nameWithoutController =
                Regex.Replace(originalName, "controller", String.Empty,
                RegexOptions.IgnoreCase);

            var newName = nameWithoutController + "Controller";
            var semanticModel = await document.GetSemanticModelAsync(cancellationToken);
            var typeSymbol = semanticModel.GetDeclaredSymbol(typeDecl, cancellationToken);

            // Produce a new solution that has all references to that type renamed, including the declaration.
            var originalSolution = document.Project.Solution;
            var optionSet = originalSolution.Workspace.Options;
            var newSolution = await Renamer.RenameSymbolAsync(document.Project.Solution, typeSymbol, newName, optionSet, cancellationToken).ConfigureAwait(false);

            // Return the new solution with the now-uppercase type name.
            return newSolution;
        }
    }
}

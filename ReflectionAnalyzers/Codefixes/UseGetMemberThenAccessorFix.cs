namespace ReflectionAnalyzers.Codefixes
{
    using System.Collections.Immutable;
    using System.Composition;
    using System.Threading.Tasks;
    using Gu.Roslyn.CodeFixExtensions;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CodeFixes;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(UseGetMemberThenAccessorFix))]
    [Shared]
    internal class UseGetMemberThenAccessorFix : DocumentEditorCodeFixProvider
    {
        private static readonly UsingDirectiveSyntax SystemReflection = SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Reflection"));

        public override ImmutableArray<string> FixableDiagnosticIds { get; } = ImmutableArray.Create(
            REFL014PreferGetMemberThenAccessor.DiagnosticId);

        protected override async Task RegisterCodeFixesAsync(DocumentEditorCodeFixContext context)
        {
            var syntaxRoot = await context.Document.GetSyntaxRootAsync(context.CancellationToken)
                                          .ConfigureAwait(false);
            foreach (var diagnostic in context.Diagnostics)
            {
                if (diagnostic.Properties.TryGetValue(nameof(ExpressionSyntax), out var expressionString) &&
                    syntaxRoot.TryFindNodeOrAncestor(diagnostic, out ExpressionSyntax old))
                {
                    context.RegisterCodeFix(
                        $"Change to: {expressionString}.",
                        (editor, _) => editor.AddUsing(SystemReflection)
                                             .ReplaceNode(old, SyntaxFactory.ParseExpression(expressionString)),
                        nameof(UseGetMemberThenAccessorFix),
                        diagnostic);
                }
            }
        }
    }
}
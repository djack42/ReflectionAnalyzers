namespace ReflectionAnalyzers
{
    using Microsoft.CodeAnalysis;

    internal static class REFL036CheckNull
    {
        public const string DiagnosticId = "REFL036";

        internal static readonly DiagnosticDescriptor Descriptor = new DiagnosticDescriptor(
            id: DiagnosticId,
            title: "Check if null before using.",
            messageFormat: "Check if null before using.",
            category: AnalyzerCategory.SystemReflection,
            defaultSeverity: DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            description: "Check if null before using.",
            helpLinkUri: HelpLink.ForId(DiagnosticId));
    }
}
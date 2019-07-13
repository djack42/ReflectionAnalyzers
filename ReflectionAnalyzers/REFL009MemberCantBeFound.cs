namespace ReflectionAnalyzers
{
    using Microsoft.CodeAnalysis;

    public static class REFL009MemberCantBeFound
    {
        public const string DiagnosticId = "REFL009";

        public static readonly DiagnosticDescriptor Descriptor = new DiagnosticDescriptor(
            id: DiagnosticId,
            title: "The referenced member is not known to exist.",
            messageFormat: "The referenced member {0} is not known to exist in {1}.",
            category: AnalyzerCategory.SystemReflection,
            defaultSeverity: DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            description: "The referenced member is not known to exist.",
            helpLinkUri: HelpLink.ForId(DiagnosticId));
    }
}

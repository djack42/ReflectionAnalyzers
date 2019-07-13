namespace ReflectionAnalyzers.Tests.REFL010PreferGenericGetCustomAttributeTests
{
    using Gu.Roslyn.Asserts;
    using Microsoft.CodeAnalysis.CodeFixes;
    using Microsoft.CodeAnalysis.Diagnostics;
    using NUnit.Framework;

    public static class CodeFix
    {
        private static readonly DiagnosticAnalyzer Analyzer = new GetCustomAttributeAnalyzer();
        private static readonly CodeFixProvider Fix = new UseGenericGetCustomAttributeFix();
        private static readonly ExpectedDiagnostic ExpectedDiagnostic = ExpectedDiagnostic.Create("REFL010");

        [Test]
        public static void WhenCast()
        {
            var code = @"
namespace RoslynSandbox
{
    using System;

    class C
    {
        public C()
        {
            var attribute = (ObsoleteAttribute)↓Attribute.GetCustomAttribute(typeof(C), typeof(ObsoleteAttribute));
        }
    }
}";
            var fixedCode = @"
namespace RoslynSandbox
{
    using System;
    using System.Reflection;

    class C
    {
        public C()
        {
            var attribute = typeof(C).GetCustomAttribute<ObsoleteAttribute>();
        }
    }
}";
            var message = "Prefer the generic extension method GetCustomAttribute<ObsoleteAttribute>.";
            RoslynAssert.CodeFix(Analyzer, Fix, ExpectedDiagnostic.WithMessage(message), code, fixedCode);
        }

        [Test]
        public static void WhenAs()
        {
            var code = @"
namespace RoslynSandbox
{
    using System;

    class C
    {
        public C()
        {
            var attribute = ↓Attribute.GetCustomAttribute(typeof(C), typeof(ObsoleteAttribute)) as ObsoleteAttribute;
        }
    }
}";
            var fixedCode = @"
namespace RoslynSandbox
{
    using System;
    using System.Reflection;

    class C
    {
        public C()
        {
            var attribute = typeof(C).GetCustomAttribute<ObsoleteAttribute>();
        }
    }
}";
            RoslynAssert.CodeFix(Analyzer, Fix, ExpectedDiagnostic, code, fixedCode);
        }
    }
}

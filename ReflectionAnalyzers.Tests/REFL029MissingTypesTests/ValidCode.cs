namespace ReflectionAnalyzers.Tests.REFL029MissingTypesTests
{
    using Gu.Roslyn.Asserts;
    using Microsoft.CodeAnalysis.Diagnostics;
    using NUnit.Framework;

    public class ValidCode
    {
        private static readonly DiagnosticAnalyzer Analyzer = new GetXAnalyzer();
        private static readonly ExpectedDiagnostic ExpectedDiagnostic = ExpectedDiagnostic.Create(REFL029MissingTypes.Descriptor);

        [Test]
        public void GetMethodNoParameter()
        {
            var code = @"
namespace RoslynSandbox
{
    using System;

    class Foo
    {
        public Foo()
        {
            var methodInfo = typeof(Foo).GetMethod(nameof(this.Bar), Type.EmptyTypes);
        }

        public int Bar() => 0;
    }
}";
            AnalyzerAssert.Valid(Analyzer, ExpectedDiagnostic, code);
        }

        [Test]
        public void GetMethodOneParameter()
        {
            var code = @"
namespace RoslynSandbox
{
    class Foo
    {
        public Foo()
        {
            var methodInfo = typeof(Foo).GetMethod(nameof(this.Id), new[] { typeof(int) });
        }

        public int Id(int value) => value;
    }
}";
            AnalyzerAssert.Valid(Analyzer, ExpectedDiagnostic, code);
        }

        [Test]
        public void GetMethodOneGenericParameter()
        {
            var code = @"
namespace RoslynSandbox
{
    class Foo
    {
        public Foo()
        {
            var methodInfo = typeof(Foo).GetMethod(nameof(this.Id));
        }

        public T Id<T>(T value) => value;
    }
}";
            AnalyzerAssert.Valid(Analyzer, ExpectedDiagnostic, code);
        }
    }
}
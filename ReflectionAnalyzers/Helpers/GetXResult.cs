namespace ReflectionAnalyzers
{
    internal enum GetXResult
    {
        Unknown,
        Single,
        NoMatch,
        ExplicitImplementation,
        Ambiguous,
        WrongFlags,
        WrongTypes,
        WrongMemberType,
        UseContainingType,
    }
}

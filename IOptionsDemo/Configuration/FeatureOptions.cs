namespace IOptionsDemo.Configuration
{
    public class FeatureOptions
    {
        public const string SectionName = "Feature";
        public bool Enabled { get; init; }
        public required string Message { get; init; }
    }
}

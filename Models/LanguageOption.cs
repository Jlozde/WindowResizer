namespace WindowResizer.Models
{
    public sealed class LanguageOption
    {
        public string Code { get; init; } = "tr-TR";
        public string Name { get; init; } = "Türkçe";
        public override string ToString() => Name;
    }
}

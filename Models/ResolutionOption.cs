namespace WindowResizer.Models
{
    public sealed class ResolutionOption
    {
        public int Width { get; init; }
        public int Height { get; init; }
        public bool IsCustom { get; init; }

        public string Display => $"{Width} × {Height}" + (IsCustom ? "  (custom)" : "");
        public override string ToString() => Display;

        public override bool Equals(object? obj)
            => obj is ResolutionOption r && r.Width == Width && r.Height == Height;

        public override int GetHashCode() => (Width, Height).GetHashCode();
    }
}

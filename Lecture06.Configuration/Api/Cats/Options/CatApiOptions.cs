namespace Lecture06.Configuration.Api.Cats.Options
{
    public class CatApiOptions
    {
        public int MaxAllowedCatCount { get; init; }

        public bool AllowDelete { get; init; }

        public bool AllowUpdate { get; init; }
    }
}

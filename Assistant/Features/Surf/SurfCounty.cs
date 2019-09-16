namespace Assistant.Features.Surf
{
    public class SurfCounty
    {
        public string Value { get; set; }

        private SurfCounty(string value) {
            Value = value;
        }

        public static SurfCounty SanDiego => new SurfCounty("san-diego");
    }
}
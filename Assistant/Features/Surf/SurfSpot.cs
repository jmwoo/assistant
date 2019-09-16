namespace Assistant.Features.Surf
{
    public class SurfSpot
    {
        public int Value { get; set; }

        private SurfSpot(int value)
        {
            Value = value;
        }

        public static SurfSpot ScrippsPier => new SurfSpot(228);
        public static SurfSpot BlacksBeach => new SurfSpot(229);
        public static SurfSpot Windansea => new SurfSpot(227);
        public static SurfSpot Tourmaline => new SurfSpot(390);
        public static SurfSpot PacificBeach => new SurfSpot(226);
    }
}
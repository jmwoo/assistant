using System.Collections.Generic;

namespace Assistant.Features.Surf {
    public class ShapeDetail {
        public string swell { get; set; }
        public string tide { get; set; }
        public string wind { get; set; }
    }

    public class SurfMoment {
        public string date { get; set; }
        public string day { get; set; }
        public string gmt { get; set; }
        public string hour { get; set; }
        public double latitude { get; set; }
        public int live { get; set; }
        public double longitude { get; set; }
        public string shape { get; set; }
        public ShapeDetail shape_detail { get; set; }
        public string shape_full { get; set; }
        public int size { get; set; }
        public double size_ft { get; set; }
        public int spot_id { get; set; }
        public string spot_name { get; set; }
        public List<object> warnings { get; set; }
    }
}
using System;

namespace Catalog.DAL.Entities {
    public class Location {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
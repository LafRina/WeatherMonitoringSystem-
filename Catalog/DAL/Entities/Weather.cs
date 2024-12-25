namespace Catalog.DAL.Entities {
    public class Weather {
        public int Id { get; set; }
        public float Temperature { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }
        public int LocationId { get; set; }
        public Location Location {get; set; }
        public int UserId { get; set; }
        public User User {get; set; }
    }
}
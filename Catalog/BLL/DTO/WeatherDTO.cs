namespace Catalog.BLL.DTO
{
    public class WeatherDTO
    {
        public int Id { get; set; }
        public float Temperature { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }
        public int LocationId { get; set; }
        public int UserId { get; set; }
    }
}
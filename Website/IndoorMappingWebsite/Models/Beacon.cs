namespace IndoorMappingWebsite.Models
{
    public class Beacon
    {
        public int id { get; set; }
        public string nome { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string localizacao { get; set; }
        public int piso { get; set; }
    }
}

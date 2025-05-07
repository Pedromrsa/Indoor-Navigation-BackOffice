namespace IndoorMappingWebsite.Models
{
    public class UserLoc
    {
        public int id { get; set; }
        public int usuarioId { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public int ?beaconId { get; set; }
        public DateTime dataHora { get; set; }
    }
   }

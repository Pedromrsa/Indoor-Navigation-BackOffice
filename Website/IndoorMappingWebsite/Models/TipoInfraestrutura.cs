namespace IndoorMappingWebsite.Models
{
    public class TipoInfraestrutura
    {
        public int id { get; set; } = -1;
        public string tipo { get; set; } = string.Empty;
    }
    public class TipoInfraestruturaSend
    {
        public string tipo { get; set; } = string.Empty;
    }
}

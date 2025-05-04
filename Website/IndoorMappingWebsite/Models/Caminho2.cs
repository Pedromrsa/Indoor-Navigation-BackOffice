namespace IndoorMappingWebsite.Models
{
    public class Caminho2
    {
        public int id { get; set; }
        public string nome { get; set; }
        public int origemId { get; set; } = -1;
        public int destinoId { get; set; } = -1;
        public bool acessivel { get; set; } = true;
        public int piso { get; set; }
        public string listaCoordenadas { get; set; }
    }
}

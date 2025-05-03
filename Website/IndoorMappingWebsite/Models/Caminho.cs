namespace IndoorMappingWebsite.Models
{
    public class Caminho
    {
        public int id { get; set; }
        public int origemId { get; set; }
        public int destinoId { get; set; }
        public int distancia { get; set; }
        public bool acessivel { get; set; } = true;
        public string tipoAcessibilidade { get; set; } = string.Empty;
    }
    public class CaminhoPost
    {
        public int id { get; set; }
        public int origemId { get; set; }
        public int destinoId { get; set; }
        public int distancia { get; set; }
        public bool acessivel { get; set; } = true;
        public int tipoAcessibilidade { get; set; } 
    }
}

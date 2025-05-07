namespace IndoorMappingWebsite.Models
{
    public class Feedback
    {
        public int id {  get; set; }
        public int usuarioId { get; set; } = -1;
        public int caminhoId { get; set; } = -1;
        public int avaliacao { get; set; } = -1;
        public string comentario { get; set; } = string.Empty;
        public DateTime dataHora { get; set; }
    }
}

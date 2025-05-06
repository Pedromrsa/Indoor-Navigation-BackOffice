namespace IndoorMappingWebsite.Models
{
    public class Feedback
    {
        public int usuarioId { get; set; } = -1;
        public int caminhoId { get; set; } = -1;
        public int avaliacao { get; set; } = -1;
        public string comentario { get; set; } = string.Empty;
    }
}

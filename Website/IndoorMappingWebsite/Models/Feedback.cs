namespace IndoorMappingWebsite.Models
{
    public class Feedback
    {
        public int id { get; set; }
        public int usuarioId { get; set; } = -1;
        public int caminhoId { get; set; } = -1;
        public int avaliacao { get; set; } = -1;
        public string comentario { get; set; } = string.Empty;
        public DateTime dataHora { get; set; }
    }
    public class FeedbackUser
    {
        public int feedbackId { get; set; }
        public int usuarioId { get; set; }
        public int adminId { get; set; }
        public string comentario { get; set; } = string.Empty;
    }
    public class FeedbackUserGet
    {
        public int id { get; set; }
        public int feedbackId { get; set; }
        public int usuarioId { get; set; }
        public int adminId { get; set; }
        public string comentario { get; set; } = string.Empty;
    }
}

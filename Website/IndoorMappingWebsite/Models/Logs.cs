using System;
using System.Numerics;

namespace IndoorMappingWebsite.Models
{
    public class Logs
    {
        public int id { get; set; }
        public int usuarioId { get; set; } 
        public String acao { get; set; } = string.Empty;
        public DateTime dataHora { get; set; } 
    }
    public class LogSend
    {
        public int usuarioId { get; set; }
        public string acao { get; set; } = string.Empty;
    }
}

using System;
using System.Numerics;

namespace IndoorMappingWebsite.Models
{
    public class User
    {
        public String nome { get; set; } = string.Empty;
        public String email { get; set; } = string.Empty;
        public String password { get; set; } = string.Empty;
        public int tipoId { get; set; }
        public int mobilidadeId { get; set; } = 1;
    }
}

using System;
using System.Numerics;

namespace IndoorMappingWebsite.Models
{
    public class Infraestrutura
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public int piso { get; set; }
        public bool acessivel { get; set; }
        public string tipoInfraestrutura { get; set; }

    }
    public class InfraestruturaSend
    {
        public string descricao { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public int piso { get; set; }
        public bool acessivel { get; set; } = true;
        public int tipoInfraestruturaId { get; set; } = -1;
    }
    public class InfraestruturaEdit
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public int piso { get; set; }
        public bool acessivel { get; set; } = true;
        public int tipoInfraestruturaId { get; set; } = -1;
    }
}

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

    public class Caminho2Full
    {
        public int id { get; set; }
        public string nome { get; set; }
        public int origemId { get; set; } = -1;
        public string origem { get; set; }
        public int destinoId { get; set; } = -1;
        public string destino { get; set; }
        public bool acessivel { get; set; } = true;
        public int piso { get; set; }
        public string listaCoordenadas { get; set; }

        public static List<Caminho2Full> mapCompleteInfo(List<Caminho2> caminhos, List<Infraestrutura> infra)
        {
            return caminhos.Select(x => new Caminho2Full {
                id = x.id,
                nome = x.nome,
                origemId = x.origemId,
                origem = GetInfraDescricao(infra, x.origemId),
                destinoId = x.destinoId,
                destino= GetInfraDescricao(infra,x.destinoId),
                acessivel = x.acessivel,
                piso = x.piso,
                listaCoordenadas = x.listaCoordenadas
            }).ToList();

        }

        private static string GetInfraDescricao(List<Infraestrutura> infras, int id)
        {
            var infra = infras.FirstOrDefault(i => i.id == id);
            return infra != null ? $"{infra.descricao}" 
                /*+
                $" (Piso {infra.piso})" */: "";
        }
    }
}

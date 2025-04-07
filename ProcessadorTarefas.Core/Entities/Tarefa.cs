using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProcessadorTarefas.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessadorTarefas.Core.Entities
{
    public class Tarefa
    {
        public Tarefa(TipoTarefa tipo, string dados)
        {
            Id = Guid.NewGuid();
            Tipo = tipo;
            Dados = dados;
            Status = StatusTarefa.Pendente;
            DataCriacao = DateTime.Now;
            DataUltimaAtualizacao = DateTime.Now;
        }

        [BsonId, BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public TipoTarefa Tipo { get; set; }
        public string Dados { get; set; }
        public StatusTarefa Status { get; set; }
        public int Tentativas { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; }
    }
}

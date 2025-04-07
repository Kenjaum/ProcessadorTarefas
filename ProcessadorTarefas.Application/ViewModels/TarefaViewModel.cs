using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessadorTarefas.Application.ViewModels
{
    public class TarefaViewModel
    {
        public Guid Id { get; set; }
        public string Tipo { get; set; }
        public string Dados { get; set; }
        public string Status { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessadorTarefas.Worker.EnviarEmail.Models
{
    public class Tarefa
    {
        public Guid Id { get; set; }
        public string Dados { get; set; }
        public StatusTarefa Status { get; set; }
        public int Tentativas { get; set; }
    }
}

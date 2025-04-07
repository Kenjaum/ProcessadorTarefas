using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessadorTarefas.Worker.EnviarEmail.Models
{
    public enum StatusTarefa
    {
        Pendente,
        EmProcessamento,
        Concluida,
        Erro
    }
}

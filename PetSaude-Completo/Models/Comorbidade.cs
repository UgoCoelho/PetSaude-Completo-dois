using System.Collections.Generic;

namespace PetSaude_Completo.Models
{
    public class Comorbidade
    {
        public int ComorbidadeId { get; set; }
        public string? Nome { get; set; }

        // 🔥 RELACIONAMENTO N:N
        public ICollection<MensagemComorbidade> MensagemComorbidades { get; set; }
            = new List<MensagemComorbidade>();
    }
}
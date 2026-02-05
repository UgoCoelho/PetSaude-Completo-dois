namespace PetSaude_Completo.Models
{
    public class MensagemComorbidade
    {
        public int MensagemId { get; set; }
        public Mensagem Mensagem { get; set; }

        public int ComorbidadeId { get; set; }
        public Comorbidade Comorbidade { get; set; }
    }
}
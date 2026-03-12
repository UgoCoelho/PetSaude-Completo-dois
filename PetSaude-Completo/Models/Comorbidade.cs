namespace PetSaude_Completo.Models
{
    public class Comorbidade
    {
        public int ComorbidadeId { get; set; }
        public string? Nome { get; set; }

        // RELACIONAMENTO COM MENSAGEM
        public ICollection<MensagemComorbidade> MensagemComorbidades { get; set; }
            = new List<MensagemComorbidade>();

        // RELACIONAMENTO COM PACIENTE
        public ICollection<PacienteComorbidade> PacienteComorbidades { get; set; }
            = new List<PacienteComorbidade>();
    }
}
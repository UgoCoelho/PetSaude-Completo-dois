namespace PetSaude_Completo.Models
{
    public class PacienteComorbidade
    {
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }

        public int ComorbidadeId { get; set; }
        public Comorbidade Comorbidade { get; set; }
    }
}
namespace PetSaude_Completo.Models
{
    public class Paciente
    {
        public int PacienteId { get; set; }
        public string? Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string? Sexo { get; set; }
        public string? Raca { get; set; }
        public string? Cpf { get; set; }
        public string? Cns { get; set; }
        public string? Celular { get; set; }
        public string? Microarea { get; set; }

        // RELACIONAMENTO N:N
        public ICollection<PacienteComorbidade> PacienteComorbidades { get; set; }
            = new List<PacienteComorbidade>();
    }
}
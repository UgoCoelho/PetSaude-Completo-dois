using System.Net;

namespace PetSaude_Completo.Models
{
    public class Paciente
    {
        public int IDPaciente { get; set; }
        public string? Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string? Sexo { get; set; }
        public string? Raca { get; set; }
        public string? Cpf { get; set; }
        public string? Cns { get; set; }
        public string? Celular { get; set; }
        public string? Microarea { get; set; }

    }
}

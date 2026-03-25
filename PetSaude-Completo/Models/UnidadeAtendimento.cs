namespace PetSaude_Completo.Models
{
    public class UnidadeAtendimento
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public Endereco Endereco { get; set; } = new Endereco(); // 👈 importante
    }
}
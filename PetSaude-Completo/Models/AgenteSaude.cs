namespace PetSaude_Completo.Models
{
    public class AgenteSaude
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }

        // Matrícula no SUS / prefeitura
        public string Matricula { get; set; }

        // Relacionamento com unidade
        public int UnidadeAtendimentoId { get; set; }
        public UnidadeAtendimento UnidadeAtendimento { get; set; }

        // Microárea que ele atende
        public Microarea Microarea { get; set; }
    }
}

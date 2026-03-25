namespace PetSaude_Completo.Models
{
    public class Microarea
    {
        public int Id { get; set; }

        public string Codigo { get; set; } // Ex: MA-001
        public string Nome { get; set; }   // Ex: "Microárea Centro 1"

        // Descrição opcional
        public string Descricao { get; set; }

        // Relacionamento com unidade
        public int UnidadeAtendimentoId { get; set; }
        public UnidadeAtendimento UnidadeAtendimento { get; set; }

        // Agente responsável
        public int? AgenteSaudeId { get; set; }
        public AgenteSaude AgenteSaude { get; set; }

        // Lista de famílias/pacientes (se quiser evoluir depois)
        public List<Paciente> Pacientes { get; set; }
    }
}

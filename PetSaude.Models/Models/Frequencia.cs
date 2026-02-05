using System.ComponentModel.DataAnnotations;

namespace PetSaude_Completo.Models
{
    public class Frequencia
    {
        [Key]
        public int FrequenciaId { get; set; }

        [Required]
        public string Nome { get; set; }

        public int Fator { get; set; }
    }

}
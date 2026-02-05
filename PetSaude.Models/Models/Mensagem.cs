using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetSaude_Completo.Models
{
    public class Mensagem
    {
        [Key]
        public int MensagemId { get; set; }

        [Required]
        [StringLength(150)]
        public string? Titulo { get; set; }

        [Required]
        [Display(Name = "Area")]
        public int AreaId { get; set; }
        public Area? Area { get; set; }

        [Required]
        [Display(Name = "Categoria")]
        public long CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        [Required]
        [Display(Name = "hora de envio")]
        public TimeSpan HorarioEnvio { get; set; }

        [Required]
        [Display(Name = "Frequencia de Mensagem")]
        public int FrequenciaMensagemId { get; set; }
        public Frequencia? FrequenciaMensagem { get; set; }

        [Required]
        public string? Conteudo { get; set; }

        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        // 🔥 RELACIONAMENTO N:N
        public ICollection<MensagemComorbidade> MensagemComorbidades { get; set; }
            = new List<MensagemComorbidade>();
    }
}
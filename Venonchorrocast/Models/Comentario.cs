namespace Venonchorrocast.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comentario")]
    public partial class Comentario
    {
        [Key]
        public int id_comentario { get; set; }

        [Display(Name = "Comentário")]
        [Required]
        [StringLength(1000)]
        public string Conteudo { get; set; }

        [Display(Name = "Horário de Publicação")]
        public DateTime Hora_Public { get; set; }

        public int? fk_id_episodio { get; set; }

        public int? fk_id_ouvinte { get; set; }

        public virtual Episodio Episodio { get; set; }

        public virtual Ouvinte Ouvinte { get; set; }
    }
}

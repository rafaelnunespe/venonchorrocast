namespace Venonchorrocast.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;


    [Table("Episodio")]
    public partial class Episodio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Episodio()
        {
            Comentario = new HashSet<Comentario>();
        }

        [Key]
        public int id_episodio { get; set; }

        [Required]
        [StringLength(50)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(500)]
        public string Descriçao { get; set; }

        public TimeSpan Duraçao { get; set; }

        [Required]
        [StringLength(200)]
        public string Link { get; set; }
        
        public byte[] Arte { get; set; }

        [Display(Name = "Data de Publicação")]
        public DateTime Data_Public { get; set; }

        [Display(Name = "Play")]
        public decimal? Qntde_Play { get; set; }

        [Display(Name = "Download")]
        public decimal? Qntde_Download { get; set; }

        [Required]
        [StringLength(500)]
        public string Participantes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comentario> Comentario { get; set; }
    }
}

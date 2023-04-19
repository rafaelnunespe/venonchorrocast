namespace Venonchorrocast.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ouvinte")]
    public partial class Ouvinte
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ouvinte()
        {
            Comentario = new HashSet<Comentario>();
        }

        [Key]
        public int id_ouvinte { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [StringLength(20)]
        public string Apelido { get; set; }

        [Required]
        [StringLength(40)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20)]
        public string Senha { get; set; }

        public byte[] Foto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comentario> Comentario { get; set; }
    }
}

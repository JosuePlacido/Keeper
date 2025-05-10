using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Enum;

namespace Models.Entities
{
    public class ChampionshipBase : IEntidade
    {
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Category Category { get; set; }
        public virtual string Season { get; set; }
        public virtual string CategoryId { get; set; }
        public virtual IEnumerable<Stage> Stages { get; set; }
        public virtual Status Status { get; set; }
        public ChampionshipBase() { }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            ChampionshipBase p2 = obj as ChampionshipBase;
            if (p2 == null)
            {
                return false;
            }
            if (base.Equals(obj))
            {
                return true;
            }
            return this.Id == p2.Id;
        }
        public override int GetHashCode()
        {
            int hash = 37;
            hash = hash * 23 + typeof(ChampionshipBase).GetHashCode();
            hash = hash * 23 + Id.GetHashCode();
            return hash;
        }
        public override string ToString()
        {
            return Name;
        }
    }
    [Table("tb_championship")]
    public class Championship : ChampionshipBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public override string Id { get; set; }
        [Required]
        [MaxLength(255)]
        public override string Name { get; set; }
        [ForeignKey("CategoriaId")]
        public override Category Category { get; set; }
        [Required]
        [MaxLength(10)]
        public override string Season { get; set; }
        public Championship() { }
    }
}

using Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class RegisterBase : IEntidade
    {
        public virtual string Id { get; set; }
        public virtual string ChampionshipId { get; set; }
        public virtual Championship Championship { get; set; }
        public virtual string TeamId { get; set; }
        public virtual Status Status { get; set; }
        public virtual Team Team { get; set; }
        [NotMapped]
        public virtual IEnumerable<RegisterPlayer> Players { get; set; }
        public virtual IEnumerable<Statistics> Statistics { get; set; }
        public RegisterBase() { }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            RegisterBase p2 = obj as RegisterBase;
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
            hash = hash * 23 + typeof(RegisterBase).GetHashCode();
            hash = hash * 23 + Id.GetHashCode();
            return hash;
        }
        public override string ToString()
        {
            return Id;
        }
    }
    [Table("tb_register")]
    public class Register : RegisterBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public override string Id { get; set; }
        [ForeignKey("ChampionshipId")]
        public override Championship Championship { get; set; }
        [ForeignKey("TeamId")]
        public override Team Team { get; set; }
        public Register() { }
    }
}

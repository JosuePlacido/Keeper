using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class PlayerBase : IEntidade
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }
        public PlayerBase() { }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            PlayerBase p2 = obj as PlayerBase;
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
            hash = hash * 23 + typeof(PlayerBase).GetHashCode();
            hash = hash * 23 + Id.GetHashCode();
            return hash;
        }
        public override string ToString()
        {
            return Name;
        }
    }
    [Table("tb_player")]
    public class Player : PlayerBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public override string Id { get; set; }

        [Required]
        [MaxLength(255)]
        public override string Name { get; set; }
        public Player() { }
    }
}

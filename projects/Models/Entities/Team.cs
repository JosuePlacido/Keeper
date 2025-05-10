using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class TeamBase : IEntidade
    {
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public TeamBase() { }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            TeamBase p2 = obj as TeamBase;
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
            hash = hash * 23 + typeof(Team).GetHashCode();
            hash = hash * 23 + Id.GetHashCode();
            return hash;
        }
        public override string ToString()
        {
            return Name;
        }
    }
    [Table("tb_team")]
    public class Team : TeamBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public override string Id { get; set; }
        [Required]
        [MaxLength(100)]
        public override string Name { get; set; }
        public Team() { }
    }
}

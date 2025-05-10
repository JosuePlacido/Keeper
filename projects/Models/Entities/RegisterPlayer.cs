using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class RegisterPlayerBase : IEntidade
    {
        public virtual string Id { get; set; }
        public virtual string RegisterId { get; set; }
        public virtual Register Register { get; set; }
        public virtual string PlayerId { get; set; }
        public virtual Player Player { get; set; }
        public virtual int Games { get; set; }
        public virtual int Goals { get; set; }
        public virtual int YellowCard { get; set; }
        public virtual int RedCard { get; set; }
        public virtual int MVPs { get; set; }
        public virtual string ChampionshipId { get; set; }
        public virtual Championship Championship { get; set; }
        public RegisterPlayerBase() { }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            RegisterPlayerBase p2 = obj as RegisterPlayerBase;
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
            hash = hash * 23 + typeof(RegisterPlayerBase).GetHashCode();
            hash = hash * 23 + Id.GetHashCode();
            return hash;
        }
        public override string ToString()
        {
            return Id;
        }
    }
    [Table("tb_register_player")]
    public class RegisterPlayer : RegisterPlayerBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public override string Id { get; set; }
        [ForeignKey("RegisterId")]
        [JsonIgnore]
        public override Register Register { get; set; }
        [ForeignKey("PlayerId")]
        public override Player Player { get; set; }
        public RegisterPlayer() { }
    }
}

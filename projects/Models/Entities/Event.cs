using Models.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class EventGameBase : IEntidade
    {
        public virtual string Id { get; set; }
        public virtual int Order { get; set; }
        public TypeEvent Type { get; set; }
        public virtual string MatchId { get; set; }
        public virtual Match Match { get; set; }
        public virtual string RegisterPlayerId { get; set; }
        public virtual RegisterPlayer RegisterPlayer { get; set; }
        public virtual string Description { get; set; }
        public virtual bool IsHomeEvent { get; set; }
        public EventGameBase() { }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            EventGameBase p2 = obj as EventGameBase;
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
            hash = hash * 23 + typeof(EventGameBase).GetHashCode();
            hash = hash * 23 + Id.GetHashCode();
            return hash;
        }
        public override string ToString()
        {
            return Id;
        }
    }
    [Table("tb_event_game")]
    public class EventGame : EventGameBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public override string Id { get; set; }
        [ForeignKey("MatchId")]
        public override Match Match { get; set; }
        [Required]
        [MaxLength(250)]
        public override string Description { get; set; }
        public EventGame() { }
    }
}

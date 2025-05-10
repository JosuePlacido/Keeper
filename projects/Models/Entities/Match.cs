using Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class MatchBase : IEntidade
    {
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Local { get; set; }
        public virtual DateTime? Date { get; set; }
        public virtual string GroupId { get; set; }
        public virtual Group Group { get; set; }
        public virtual Vacancy HookHome { get; set; }
        public virtual string HookHomeId { get; set; }
        public virtual Vacancy HookAway { get; set; }
        public virtual string HookAwayId { get; set; }
        public virtual string HomeId { get; set; }
        public virtual Register Home { get; set; }
        public virtual string AwayId { get; set; }
        public virtual Register Away { get; set; }
        public virtual int Round { get; set; }
        public virtual int? GoalsHome { get; set; }
        public virtual int? GoalsAway { get; set; }
        public virtual int? GoalsPenaltyHome { get; set; }
        public virtual int? GoalsPenaltyVisitante { get; set; }
        public virtual bool FinalGame { get; set; }
        public virtual bool AggregateGame { get; set; }
        public virtual bool Penalty { get; set; }
        public virtual int? AggregateGoalsAway { get; set; }
        public virtual int? AggregateGoalsHome { get; set; }
        public virtual Status Status { get; set; }
        public virtual IEnumerable<EventGame> EventGame { get; set; }
        public MatchBase() { }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            MatchBase p2 = obj as MatchBase;
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
            hash = hash * 23 + typeof(MatchBase).GetHashCode();
            hash = hash * 23 + Id.GetHashCode();
            return hash;
        }
        public override string ToString()
        {
            return Name;
        }
    }
    [Table("tb_match")]
    public class Match : MatchBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public override string Id { get; set; }
        [Required]
        [MaxLength(255)]
        public override string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public override string Local { get; set; }
        [DataType(DataType.DateTime)]
        public override DateTime? Date { get; set; }
        [ForeignKey("GroupId")]
        public override Group Group { get; set; }
        [ForeignKey("HomeId")]
        public override Register Home { get; set; }
        [ForeignKey("AwayId")]
        public override Register Away { get; set; }
        [Required]
        public override bool Penalty { get; set; }
        public Match() { }
    }
}

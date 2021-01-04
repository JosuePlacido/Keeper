using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class StatisticsBase : IEntidade
    {
        public virtual string Id { get; set; }
        public virtual string GroupId { get; set; }
        [JsonIgnore]
        public virtual Group Group { get; set; }
        public virtual string RegisterId { get; set; }
        [JsonIgnore]
        public virtual Register Register { get; set; }
        public virtual int Games { get; set; }
        public virtual int Won { get; set; }
        public virtual int Drowns { get; set; }
        public virtual int Lost { get; set; }
        public virtual int GoalsScores { get; set; }
        public virtual int Position { get; set; }
        public virtual int GoalsAgainst { get; set; }
        public virtual int GoalsDifference { get; set; }
        public virtual int Yellows { get; set; }
        public virtual int Reds { get; set; }
        public virtual int Points { get; set; }
        public StatisticsBase() { }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            StatisticsBase p2 = obj as StatisticsBase;
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
            hash = hash * 23 + typeof(StatisticsBase).GetHashCode();
            hash = hash * 23 + Id.GetHashCode();
            return hash;
        }
        public override string ToString()
        {
            return Id;
        }
    }
    [Table("tb_statistics")]
    public class Statistics : StatisticsBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public override string Id { get; set; }
        [ForeignKey("GroupId")]
        [JsonIgnore]
        public override Group Group { get; set; }
        [ForeignKey("RegisterId")]
        [JsonIgnore]
        public virtual Register Register { get; set; }
        public Statistics()
        {
            Games = 0;
            Victorys = 0;
            Draws = 0;
            Defeats = 0;
            GoalsScores = 0;
            Position = 0;
            GoalsAgainst = 0;
            GoalsDifference = 0;
            Yellows = 0;
            Reds = 0;
            Points = 0;
        }
    }
}

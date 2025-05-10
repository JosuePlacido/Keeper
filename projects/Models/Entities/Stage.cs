using Models.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class StageBase : IEntidade
    {
        public virtual string Id { get; set; }
        public virtual int Order { get; set; }
        public virtual string Name { get; set; }
        public virtual Championship Championship { get; set; }
        public virtual string ChampionshipId { get; set; }
        public virtual int Teams { get; set; }
        public virtual int SpotsNextStage { get; set; }
        public virtual TypeStage TypeStage { get; set; }
        public virtual bool IsDoubleTurn { get; set; }
        public virtual string Criterias { get; set; }
        public virtual Classifieds Regulation { get; set; }
        public virtual IEnumerable<Group> Group { get; set; }
        public StageBase() { }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            StageBase p2 = obj as StageBase;
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
            hash = hash * 23 + typeof(StageBase).GetHashCode();
            hash = hash * 23 + Id.GetHashCode();
            return hash;
        }
        public override string ToString()
        {
            return Name;
        }
    }
    [Table("tb_stage")]
    public class Stage : StageBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public override string Id { get; set; }
        [Required]
        [MaxLength(50)]
        public override string Name { get; set; }
        [ForeignKey("ChampionshipId")]
        public override Championship Championship { get; set; }
        public Stage() { }
    }
}

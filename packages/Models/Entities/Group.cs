using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class GroupBase : IEntidade
    {
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string StageId { get; set; }
        public virtual Stage Stage { get; set; }
        public virtual IEnumerable<Register> Teams { get; set; }
        public virtual IEnumerable<HookPlaces> HookPlaces { get; set; }
        public virtual IEnumerable<Statistics> Statistics { get; set; }
        public virtual IEnumerable<Match> Matchs { get; set; }
        public GroupBase() { }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            GroupBase p2 = obj as GroupBase;
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
            hash = hash * 23 + typeof(GroupBase).GetHashCode();
            hash = hash * 23 + Id.GetHashCode();
            return hash;
        }
        public override string ToString()
        {
            return Name;
        }
    }
    [Table("tb_group")]
    public class Group : GroupBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public override string Id { get; set; }
        [Required]
        [MaxLength(50)]
        public override string Name { get; set; }
        [ForeignKey("StageId")]
        public override Stage Stage { get; set; }
        public Group() { }
    }
}

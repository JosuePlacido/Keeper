using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class NewsBase : IEntidade
    {
        public virtual string Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime Date { get; set; }
        public NewsBase() { }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            NewsBase p2 = obj as NewsBase;
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
            hash = hash * 23 + typeof(NewsBase).GetHashCode();
            hash = hash * 23 + Id.GetHashCode();
            return hash;
        }
        public override string ToString()
        {
            return Title;
        }
    }
    [Table("tb_news")]
    public class news : NewsBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public override string Id { get; set; }
        [Required]
        [MaxLength(100)]
        public override string Title { get; set; }
        public news() { }
    }
}

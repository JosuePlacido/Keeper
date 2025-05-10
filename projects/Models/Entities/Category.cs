using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class CategoryBase : IEntidade
    {
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public CategoryBase() { }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            CategoryBase p2 = obj as CategoryBase;
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
            hash = hash * 23 + typeof(CategoryBase).GetHashCode();
            hash = hash * 23 + Id.GetHashCode();
            return hash;
        }
        public override string ToString()
        {
            return Name;
        }
    }
    [Table("tb_category")]
    public class Category : CategoryBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public override string Id { get; set; }

        [Required]
        [MaxLength(100)]
        public override string Name { get; set; }
        public Category() { }
    }
}

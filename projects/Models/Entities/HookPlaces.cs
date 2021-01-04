using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class VacancyBase : IEntidade
    {
        public virtual string Id { get; set; }
        public virtual string Description { get; set; }
        public virtual Classifieds Regulation { get; set; }
        public virtual Group FromGroup { set; get; }
        public virtual string FromGroupId { set; get; }
        public virtual Models.OwnGroup { set; get; }
    public virtual string OwnGroupId { set; get; }
    public virtual int Position { get; set; }
    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        VacancyBase p2 = obj as VacancyBase;
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
        hash = hash * 23 + typeof(VacancyBase).GetHashCode();
        hash = hash * 23 + Id.GetHashCode();
        return hash;
    }
    public override string ToString()
    {
        return Description;
    }
}
[Table("tb_hook_place")]
public class Vacancy : VacancyBase
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public override string Id { get; set; }
    [Required]
    [MaxLength(50)]
    public override string Description { get; set; }
    [ForeignKey("FromGroupId")]
    public override Group FromGroup { get; set; }

    public Vacancy() { }
}
}

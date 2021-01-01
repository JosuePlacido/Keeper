using Models.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class ADBase : IEntidade
    {
        public virtual string Id { get; set; }
        public virtual string Link { get; set; }
        public virtual string Anunciante { get; set; }
        public virtual string Img { get; set; }
        public virtual IEnumerable<Horario> Horarios { get; set; }
        public virtual bool Ativo { get; set; }
        public ADBase() { }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            ADBase p2 = obj as ADBase;
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
            hash = hash * 23 + typeof(ADBase).GetHashCode();
            hash = hash * 23 + Id.GetHashCode();
            return hash;
        }
        public override string ToString()
        {
            return Link;
        }
    }
    [Table("tb_ad")]
    public class AD : ADBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public override string Id { get; set; }
        public AD() { }
    }
}

using Models.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class HorarioBase : IEntidade
    {
        public virtual string Id { get; set; }
        public virtual string ADId { get; set; }
        public virtual AD AD { get; set; }
        public virtual int Dia { get; set; }
        public virtual int HoursBegin { get; set; }
        public virtual int HoursEnd { get; set; }
        public HorarioBase() { }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            HorarioBase p2 = obj as HorarioBase;
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
            hash = hash * 23 + typeof(HorarioBase).GetHashCode();
            hash = hash * 23 + Id.GetHashCode();
            return hash;
        }
        public override string ToString()
        {
            return $"{(DayOfWeek)Dia},{HoursBegin}:00 até {HoursEnd}:00";
        }
    }
    [Table("tb_agenda")]
    public class Horario : HorarioBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public override string Id { get; set; }
        [JsonIgnore]
        public override AD AD { get; set; }
        public Horario() { }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace TestCase66bit.Models
{
    public class FootballPlayerModel
    {
        public int Id { get; set; }
        [MaxLength(30), Required, DisplayName("Имя")]
        public string firstName { get; set; }
        [MaxLength(30), Required, DisplayName("Фамилия")]
        public string lastName { get; set; }
        [MaxLength(8), Required, DisplayName("Пол")]
        public string gender { get; set; }

        [Column(TypeName = "date"), DisplayName("Дата рождения")]
        public DateTime birthDate { get; set; }
        [MaxLength(30), DisplayName("Команда")]
        public string team { get; set; }
        [DisplayName("Страна")]
        public string country { get; set; }
 
    }
}

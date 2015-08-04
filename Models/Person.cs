using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace TestPerson.Models
{
    public class Person
    {
        [Required]
        [Display(Name = "Nombre de Persona")]
        public String Nombre { get; set; }
        public String FechaNac { get; set; }
        public Byte[] Pic { get; set; }
        public int Id { get; set; }
        public Person()
        {
            Pic = null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DA2_SistemaEscolar2016.Models
{

    public class Alumno
    {
        
        [Key] //llave primaria con autoincremento de uno en uno;
        public int numeroMatricula { get; set; }
        [Required]
        [MinLength(1)]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Required]
        [Display(Name ="Apellido Paterno")]
        public string apellidoPaterno { get; set; }

        [Required]
        [Display(Name = "Apellido Materno")]
        public string apellidoMaterno { get; set; }

        [Required]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        [DataType(DataType.Date)]
        public DateTime fechaDeNacimiento { get; set; }

        //un alumno pertenece a un solo grupo;
        [Required]
        [Display(Name ="Grupo")]
        public int grupoID { get; set; }
        virtual public Grupo grupo { get; set; } 

    }
}
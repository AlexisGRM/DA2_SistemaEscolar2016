using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA2_SistemaEscolar2016.Models
{
    public class Grupo
    {
        //llave primaria;
        public int grupoID { get; set; }
        
        public String nombreGrupo { get; set; }
        public String carrera { get; set; }
        
        // un grupo tiene muchos alumnos;
        public virtual ICollection<Alumno> alumnos { get; set; }
    }

}
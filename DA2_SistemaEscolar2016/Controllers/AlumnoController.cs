﻿using DA2_SistemaEscolar2016.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DA2_SistemaEscolar2016.Controllers
{
    [Authorize]
    public class AlumnoController : Controller
    {
        //conectarnos a la BD
        ApplicationDbContext db = new ApplicationDbContext();

        [HttpPost]
        
        public ActionResult Listar(string nombreBuscado)
        {
            var resultadoDeBusqueda = new List<Alumno>();

            //consultar la lista de alumnos
            //select * FROM alumnos
            if (!string.IsNullOrEmpty(nombreBuscado)) {
                //var resusltadoDeBusqueda = db.alumnos.Where(a => a.nombre == nombreBuscado || a.apellidoPaterno == nombreBuscado
                //|| a.apellidoMaterno == nombreBuscado).ToList();

                //resultadoDeBusqueda = db.alumnos.Where(a => (a.apellidoPaterno + a.apellidoMaterno + a.nombre).Contains(nombreBuscado)).ToList();
                resultadoDeBusqueda = db.alumnos.Where(a => a.apellidoMaterno.Contains(nombreBuscado) ||
                a.apellidoMaterno.Contains(nombreBuscado) || 
                a.nombre.Contains(nombreBuscado)).ToList();
            }
            else {
                resultadoDeBusqueda = db.alumnos.ToList();

            }

            //pedirle a la lista que muestre los resultados en pantalla

            return View(resultadoDeBusqueda);
        }

        [HttpGet]
        public ActionResult Listar()
        {


            //consultar la lista de alumnos
            //select * FROM alumnos
            var todosLosAlumnos = db.alumnos.ToList();
           

            //pedirle a la lista que muestre los resultados en pantalla

            return View(todosLosAlumnos);
        }

        //este te muestra con la pagina con la forma para dar de alta aun nuevo alumno
        [HttpGet]
        [Authorize(Roles="Admin ,Capturista")]
        public ActionResult crear()
        {
            var grupos = db.grupos;
            SelectList grupoID = new SelectList(grupos, "grupoID", "nombreGrupo");

            ViewBag.grupoID = grupoID;
            return View();
        }

        //este se encarga de recibir los datos del nuevo alumno y guardarlo
        [HttpPost]
        public ActionResult crear(Alumno alumnoNuevo, bool enDetallesDeGrupo=false)
        {
            if (ModelState.IsValid) { 
            //Crear Alumno
            db.alumnos.Add(alumnoNuevo);
            
            //Guardar Cambios
            db.SaveChanges();
                if (enDetallesDeGrupo)
                {
                    return RedirectToAction("detalles", "grupos", new { id = alumnoNuevo.grupoID });
                }
                else
                {
                    //Regresar una Vista
                    return RedirectToAction("listar");
                }
                //Regresar una Vista
                
                
            }
            ViewBag.MensajeError = "Hubo un error,Favor de verificar la informacion";

            var grupos = db.grupos;
            SelectList grupoID = new SelectList(grupos, "grupoID", "nombreGrupo");

            ViewBag.grupoID = grupoID;
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult eliminar(int id=0)
        {
            var alumno = db.alumnos.Find(id);
            if (alumno == null)
            {
                return RedirectToAction("listar");
            }else
            {
                return View(alumno);
            }

           
        }

        [HttpPost]
        [ActionName("eliminar")]
        [Authorize(Roles = "Admin")]
        public ActionResult confirmarEliminar( int numeroMatricula=0)
        {

            var alumno = db.alumnos.Find(numeroMatricula);

            if (alumno == null)
            {
                return RedirectToAction("listar");
            }

            db.alumnos.Remove(alumno);
            db.SaveChanges();//Ejecuta la lista de querys en la BD
            return RedirectToAction("listar");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult editar(int id=0)
        {
            var alumno = db.alumnos.Find(id);
                if (alumno == null)
            {
                return RedirectToAction("listar");
            }
            var grupo = db.grupos;
            SelectList grupoID = new SelectList(grupo, "grupoID", "nombreGrupo");
            ViewBag.grupoID = grupoID;
            return View(alumno);
        }

        [HttpPost]
        [Authorize(Roles = "Admin ")]
        public ActionResult editar(Alumno alumnoEditado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alumnoEditado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("listar");
            }
            return View();
        }


    }   


}
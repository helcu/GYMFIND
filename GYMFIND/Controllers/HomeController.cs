using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYMFIND.ViewModel;
using GYMFIND.Models;
using System.IO;

using System.Drawing;
using System.Drawing.Imaging; 

namespace GYMFIND.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login() {


            return View();

        }

        public ActionResult dashboard() {


            return View();
        }


        [HttpPost]
        public ActionResult login(VmLogin vmLogin) {

            try
            {
                GYMEntities context = new GYMEntities();

                Cliente cliente=  context.Cliente.FirstOrDefault(x => x.Usuario == vmLogin.usuario && x.Clave == vmLogin.clave);
                if (cliente == null)
                {
                    Asociado asociado = context.Asociado.FirstOrDefault(x => x.Usuario == vmLogin.usuario && x.Clave == vmLogin.clave);
                    if(asociado == null)
                    return View(vmLogin);

                    Session["objUsuario"] = asociado;
                    Session["rol"] = "A";
                    return RedirectToAction("dashboard");
                }


                    Session["objUsuario"] = cliente;
                Session["rol"] = "C";
                    return RedirectToAction("dashboard");

                

            }
            catch (Exception)
            {

                return View(vmLogin);
            }

             }


        [HttpPost]
        public ActionResult registrarUsuario(VmLogin vmLogin)
        {
            try
            {
                Cliente cliente = new Cliente();
                cliente.Usuario = vmLogin.usuario;
                cliente.Clave = vmLogin.clave;
                cliente.Correo = vmLogin.correo;
                cliente.IDApi = null;
                cliente.Foto = "~/Content/images/user.png";
                //string imageFile = System.Web.HttpContext.Current.Server.MapPath("~/Content/images/user.png");
                //var srcImage = Image.FromFile(imageFile);
                //var stream = new MemoryStream();
                //srcImage.Save(stream, ImageFormat.Png);
                //cliente.Foto= stream.ToArray();
                cliente.Rol = "User";

                GYMEntities context = new GYMEntities();

                context.Cliente.Add(cliente);
                context.SaveChanges();
                Session["objUsuario"] = cliente;
                return RedirectToAction("dashboard");
                

                
            }
            catch (Exception ex)
            {

                return View(vmLogin);
            }

            
        }

        public ActionResult registrarPlan() {

            VmRegistrarPlan vmRegistrar = new VmRegistrarPlan();

            GYMEntities context = new GYMEntities();
             vmRegistrar.lista = context.Categoria.ToList();
       
           
          //Session["establecimiento"] = ((Asociado)Session["objUsuario"]).Establecimiento;

            //ViewData["categoria"] = new SelectList(lst,"CategoriaID","Nombre");

            return View(vmRegistrar);
        }
        [HttpPost]
        public ActionResult registrarPlan(VmRegistrarPlan vmRegistrarPlan) {

            try
            {
                GYMEntities context = new GYMEntities();


                Planes obj = new Planes();

                context.Planes.Add(obj);

                obj.Nombre = vmRegistrarPlan.nombre;
                obj.Descripcion = vmRegistrarPlan.descripcion;
                obj.Costo = vmRegistrarPlan.costo;

                obj.EstablecimientoID = ((Asociado)Session["objUsuario"]).EstablecimientoID;
                obj.categoriaID = vmRegistrarPlan.categoria;

                context.SaveChanges();

                return RedirectToAction("dashboard");
            }
            catch (Exception)
            {

                return View("dashboard");
            }

          

            
        }

    }
}
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
using BarcodeLib;
using QRCoder;


namespace GYMFIND.Controllers
{
    public class HomeController : Controller
    {
        GYMEntities context = new GYMEntities();
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
                    return RedirectToAction("gimnacioMapa");

                

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

        public ActionResult registrarPlan(int? planId) {
            GYMEntities context = new GYMEntities();
            VmRegistrarPlan vmRegistrar = new VmRegistrarPlan();
            vmRegistrar.Fill(context, planId);

            //GYMEntities context = new GYMEntities();
             //vmRegistrar.lista = context.Categoria.ToList();
       
           
          //Session["establecimiento"] = ((Asociado)Session["objUsuario"]).Establecimiento;

            //ViewData["categoria"] = new SelectList(lst,"CategoriaID","Nombre");

            return View(vmRegistrar);
        }
        [HttpPost]
        public ActionResult registrarPlan(VmRegistrarPlan vmRegistrarPlan) {

            try
            {
                
                Planes obj = null;
                if (vmRegistrarPlan.planID.HasValue)
                {
                    obj = context.Planes.FirstOrDefault(x => x.PlanID == vmRegistrarPlan.planID);
                }
                else {

                    obj = new Planes();
                    context.Planes.Add(obj);
                }
                

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
                vmRegistrarPlan.Fill(context,null);
                TryUpdateModel(vmRegistrarPlan);
                return View(vmRegistrarPlan);
            }

          

            
        }

        public ActionResult listarPlan() {

            VmListarPlan vmRegistrarPlan = new VmListarPlan();
            
            vmRegistrarPlan.fill(((Asociado)Session["objUsuario"]).EstablecimientoID);

            return View(vmRegistrarPlan);
        }
        [HttpPost]
        public ActionResult listarPlan(VmListarPlan vmRegistrarPlan)
        {

           

            vmRegistrarPlan.fill(((Asociado)Session["objUsuario"]).EstablecimientoID);

            return View(vmRegistrarPlan);
        }
        public ActionResult gimnacioMapa() {

            VmGimnacioMapa vmGimnacionMapa = new VmGimnacioMapa();

            vmGimnacionMapa.fill();


            return View(vmGimnacionMapa);
        }
        public ActionResult gimnaciosBusqueda() {

            VmGimnaciosBusqueda vmGimnaciosBusqueda = new VmGimnaciosBusqueda();
            vmGimnaciosBusqueda.fill();


            return View(vmGimnaciosBusqueda);
        }
        [HttpPost]
        public ActionResult gimnaciosBusqueda(VmGimnaciosBusqueda vmGimnaciosBusqueda)
        {

            
            vmGimnaciosBusqueda.fill();


            return View(vmGimnaciosBusqueda);
        }

        public ActionResult establecimientoInfo(int establecimientoID) {

            VmEstablecimientoInfo vmEsablecimientoInfo = new VmEstablecimientoInfo();

            vmEsablecimientoInfo.establecimientoID = establecimientoID;

            vmEsablecimientoInfo.fill();

            return View(vmEsablecimientoInfo);
        }

        public ActionResult compraPlan(int planID) {
            VmCompraPlan vmComprarPlan = new VmCompraPlan();

            vmComprarPlan.fill(planID);

            return View(vmComprarPlan);
        }
        [HttpPost]
        public ActionResult compraPlan(VmCompraPlan vmComprarPlan)
        {
            try
            {
                Compra compra = new Compra();
                compra.ClienteID = ((Cliente)Session["objUsuario"]).ClienteID;
                compra.PlanID = vmComprarPlan.planID;
                //insertar codigo de generacion de cosigo QR
                //BarcodeLib. qrbarcode = new BarcodeLib.Barcode();

                //qrbarcode.
                //BarCode qrcode = new BarCode();
                //qrcode.Symbology = KeepAutomation.Barcode.Symbology.QRCode;


                PlanPagadoViewModel objViewModel = new PlanPagadoViewModel();
                Cliente objCliente = (Cliente)Session["objCliente"];
                string qrString = "Cliente ID: " + objCliente.IDCliente + " Nombre: " + objCliente.Nombre + " " + objCliente.Apellido + "\n" + "Plan ID: " + idPlan + " Nombre: " + nombrePlan;
                QRCodeGenerator qrc = new QRCodeGenerator();
                QRCodeGenerator.QRCode qc = qrc.CreateQrCode(qrString, QRCodeGenerator.ECCLevel.Q);
                Bitmap bmp = qc.GetGraphic(20);
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, ImageFormat.Png);
                byte[] bt = ms.ToArray();



                compra.QR = "codigo :v";

                context.Compra.Add(compra);

                context.SaveChanges();

                return RedirectToAction("gimnacioMapa");
            }
            catch (Exception)
            {

                return View();
            }

        }

            public ActionResult planesAdquiridos() {

            VmPlanesAdquiridos VmPlanesAdquiridos = new VmPlanesAdquiridos();

            VmPlanesAdquiridos.fill(((Cliente)Session["objUsuario"]).ClienteID);




            return View(VmPlanesAdquiridos);
        }
           
        }
    }

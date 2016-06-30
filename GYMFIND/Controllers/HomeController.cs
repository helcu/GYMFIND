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
        //[Authorize]
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
                    if (asociado == null)
                    {
                        Administrador administrador = context.Administrador.FirstOrDefault(x=>x.Usuario == vmLogin.usuario&& x.Clave==vmLogin.clave);
                        if (administrador == null)
                         return View(vmLogin);
                        Session["objUsuario"] = administrador;
                        Session["rol"] = "D";
                        return RedirectToAction("dashboard");

                    }

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
                cliente.Foto = "/Content/images/user.png";
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
                Session["rol"] = "C";
                return RedirectToAction("gimnacioMapa");
                

                
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
        public ActionResult registrarPlan( VmRegistrarPlan vmRegistrarPlan,HttpPostedFileBase file ) {

            try
            {
                
                Planes obj = null;
                if (vmRegistrarPlan.planID.HasValue)
                {
                    obj = context.Planes.FirstOrDefault(x => x.PlanID == vmRegistrarPlan.planID);

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/img"), fileName);
                        file.SaveAs(path);
                        obj.imagen = fileName;
                    }
                    //else
                    //{

                    //    obj.imagen = "portfolio5.jpg";
                    //}
                }
                else {

                    obj = new Planes();
                    context.Planes.Add(obj);

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/img"), fileName);
                        file.SaveAs(path);
                        obj.imagen = fileName;
                    }
                    else
                    {

                        obj.imagen = "portfolio5.jpg";
                    }

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

                
                //QRCodeGenerator qrc = new QRCodeGenerator();
                //QRCodeGenerator.QRCode qc = qrc.CreateQrCode(compra.PlanID.ToString(), QRCodeGenerator.ECCLevel.Q);
                //Bitmap bmp = qc.GetGraphic(20);
                
                //MemoryStream ms = new MemoryStream();
              
                //bmp.Save(ms, ImageFormat.Png);
                
                //byte[] bt = ms.ToArray();

                //Image i = new Image();
               
                //var filename = Path.GetFileName(bt.FileName);
                //var path = Path.Combine(Server.MapPath("~/Content/image/"), filename);
                //file.SaveAs(path);
                //tyre.Url = filename;

                //_db.EventModels.AddObject(eventmodel);
                //_db.SaveChanges();
                //return RedirectToAction("Index");


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

//respnde una imagen conelid de peticion y genera un codigo q a paritr de esto

        public ActionResult getQr(int id) {

            string text = id.ToString();

            QRCodeGenerator qrc = new QRCodeGenerator();
            QRCodeGenerator.QRCode qc = qrc.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            Bitmap bmp = qc.GetGraphic(20);

            MemoryStream ms = new MemoryStream();

            bmp.Save(ms, ImageFormat.Png);

            byte[] bt = ms.ToArray();
            return File(bt, "image/png");

        }

        [HttpPost]
        public ActionResult loginFB(VmLogin vmLogin) {

            Cliente cliente = context.Cliente.FirstOrDefault(x=>x.IDApi==vmLogin.FbID);

            if (cliente==null)
            {
                cliente = new Cliente();
                cliente.Usuario = vmLogin.usuario;
                cliente.Clave = "";
                cliente.Correo = vmLogin.correo;
                cliente.IDApi = vmLogin.FbID;
                cliente.Foto = vmLogin.imagen;
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
                Session["rol"] = "C";
                return RedirectToAction("gimnacioMapa");

            }

            Session["objUsuario"] = cliente;
            Session["rol"] = "C";
            return RedirectToAction("gimnacioMapa");


            //return View();
        }


        public ActionResult cerrarSesion() {




            Session.Clear();
            return RedirectToAction("login");

        }

        public ActionResult listarAsociados() {
            VmListarAsociados vmListarAsociados = new VmListarAsociados();

            vmListarAsociados.fill();

            return View(vmListarAsociados);
        }


        public ActionResult registrarAsociados(int? AsociadoID)
        {
            ViewModelRegistrarAsociado viewModelRegistrarAsociado = new ViewModelRegistrarAsociado();

            viewModelRegistrarAsociado.fill(context, AsociadoID);

            return View(viewModelRegistrarAsociado);
        }

        [HttpPost]
        public ActionResult registrarAsociados(ViewModelRegistrarAsociado viewModelRegistrarAsociado, HttpPostedFileBase file)
        {


            try
            {

                Asociado obj = null;
                if (viewModelRegistrarAsociado.asociadoID.HasValue)
                {
                    obj = context.Asociado.FirstOrDefault(x => x.AsociadoID == viewModelRegistrarAsociado.asociadoID);

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);
                        file.SaveAs(path);
                        obj.Foto = "~/Content/images/" + fileName;
                    }
                    //else
                    //{

                    //    obj.imagen = "portfolio5.jpg";
                    //}
                }
                else
                {

                    obj = new Asociado();
                    context.Asociado.Add(obj);

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);
                        file.SaveAs(path);
                        obj.Foto = "~/Content/images/" + fileName;
                    }
                    else
                    {

                        obj.Foto = "~/Content/images/user.png";
                    }

                }

                obj.Usuario = viewModelRegistrarAsociado.usuario;
                obj.Clave = viewModelRegistrarAsociado.clave;
                obj.EstablecimientoID = viewModelRegistrarAsociado.establecimientoId;
                obj.Rol = "A";
                
                

                context.SaveChanges();

                return RedirectToAction("listarAsociados");
            }
            catch (Exception)
            {
                viewModelRegistrarAsociado.fill(context, null);
                TryUpdateModel(viewModelRegistrarAsociado);
                return View(viewModelRegistrarAsociado);
            }
        }

        public ActionResult listarEstablecimiento()
        {

            VmGimnaciosBusqueda vmGimnaciosBusqueda = new VmGimnaciosBusqueda();
            vmGimnaciosBusqueda.fill();


            return View(vmGimnaciosBusqueda);
        }

        public ActionResult agregarEstablecimiento(int? establecimientoID) {

            VmRegistrarEstablecimiento vmRegistrarEstablecimiento = new VmRegistrarEstablecimiento();

            vmRegistrarEstablecimiento.fill(context,establecimientoID);

            return View(vmRegistrarEstablecimiento);




        }
        [HttpPost]
        public ActionResult agregarEstablecimiento(VmRegistrarEstablecimiento vmRegistrarEstablecimiento, HttpPostedFileBase file)
        {


            try
            {

                Establecimiento obj = null;
                if (vmRegistrarEstablecimiento.establecimientoID.HasValue)
                {
                    obj = context.Establecimiento.FirstOrDefault(x => x.EstablecimientoID == vmRegistrarEstablecimiento.establecimientoID);

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);
                        file.SaveAs(path);
                        obj.imagen = "~/Content/images/" + fileName;
                    }
                    //else
                    //{

                    //    obj.imagen = "portfolio5.jpg";
                    //}
                }
                else
                {

                    obj = new Establecimiento();
                    context.Establecimiento.Add(obj);

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);
                        file.SaveAs(path);
                        obj.imagen = "~/Content/images/" + fileName;
                    }
                    else
                    {

                        obj.imagen = "~/Content/images/4.jpg";
                    }

                }

                obj.Nombre = vmRegistrarEstablecimiento.nombre;
                obj.Direccion = vmRegistrarEstablecimiento.direccion;
                obj.RUC = vmRegistrarEstablecimiento.ruc;
                obj.Latitud = vmRegistrarEstablecimiento.latitud;
                obj.Longitud = vmRegistrarEstablecimiento.longitud;
                obj.Portal = vmRegistrarEstablecimiento.portal;




                context.SaveChanges();

                return RedirectToAction("listarEstablecimiento");
            }
            catch (Exception)
            {
                vmRegistrarEstablecimiento.fill(context, null);
                TryUpdateModel(vmRegistrarEstablecimiento);
                return View(vmRegistrarEstablecimiento);
            }

        }

    }
}

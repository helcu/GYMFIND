using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GYMFIND.Models;
using GYMFIND.Helpers;
namespace GYMFIND.ViewModel
{
    public class VmPlanesAdquiridos
    {
        public List<CrossCompra> listac { get; set; }


        public void fill(int clienteid) {
            listac = new List<CrossCompra>();
            GYMEntities context = new GYMEntities();

            List<Compra> lista = context.Compra.Where(x=> x.ClienteID==clienteid).ToList();
            CrossCompra obj = null;

            foreach (var p in lista)
            {
                obj = new CrossCompra();
                obj.compraID = p.CompraID;
                obj.qr = p.QR;
                

                    Planes plan = context.Planes.FirstOrDefault(x => x.PlanID == p.PlanID);
                obj.nombre = plan.Nombre;
                obj.costo = plan.Costo;
                Establecimiento esta = context.Establecimiento.FirstOrDefault(x => x.EstablecimientoID == plan.EstablecimientoID);

                obj.local = esta.Nombre;

                this.listac.Add(obj);

            }

        }

    }
}
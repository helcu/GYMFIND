using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GYMFIND.Models;


namespace GYMFIND.ViewModel
{
    public class VmEstadoCuenta
    {



        public List<Compra> lista { get; set; }

        public void fill(int estaID) {

            GYMEntities context = new GYMEntities();

            this.lista = context.Compra.Where(x => x.Planes.EstablecimientoID == estaID).ToList();


        }
    }
}
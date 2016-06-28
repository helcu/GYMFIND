using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GYMFIND.Models;
namespace GYMFIND.ViewModel
{
    public class VmEstablecimientoInfo
    {
        public int  establecimientoID { get; set; }


        public List<Planes> lista { get; set; }


        public void fill() {
            GYMEntities context = new GYMEntities();


            lista = context.Planes.Where(x => x.EstablecimientoID == establecimientoID).ToList();

        }

    }
}
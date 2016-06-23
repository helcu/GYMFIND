using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GYMFIND.Models;
namespace GYMFIND.ViewModel
{
    public class VmListarPlan
    {

        public List<Planes> lista{ get; set; }
        public String filtro { get; set; }

        public void fill(int estableciminetoID) {

            GYMEntities context = new GYMEntities();


            var query = context.Planes.Where(x => x.EstablecimientoID == estableciminetoID).AsQueryable();

            if (!String.IsNullOrEmpty(filtro))
            {

                query = query.Where(x => x.Nombre.Contains(filtro.ToUpper()));
            }

            this.lista = query.ToList();

            //foreach (var item in lista)
            //{
            //    item.PlanID
            //}

        }


    }
}
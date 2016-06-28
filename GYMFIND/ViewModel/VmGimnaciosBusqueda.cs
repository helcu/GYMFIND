using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GYMFIND.Models;
namespace GYMFIND.ViewModel
{
    public class VmGimnaciosBusqueda
    {

        public String  filtro { get; set; }
        public List<Establecimiento> lista { get; set; }


        public void fill(){

            GYMEntities context = new GYMEntities();

            var query = context.Establecimiento.AsQueryable();
            if (!String.IsNullOrEmpty(filtro)) {

                query = query.Where(x => x.Nombre.Contains(filtro.ToUpper()));

            }
            lista = query.ToList();

        }



    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYMFIND.Models;

namespace GYMFIND.ViewModel
{
    //[Bind(Exclude = "lista")]
    public class VmRegistrarPlan
    {
        public String nombre { get; set; }
        public String descripcion { get; set; }
        public double costo { get; set; }
        public int establecimiento { get; set; }
        public int categoria { get; set; }

        public List<Categoria> lista  { get; set; }

        public VmRegistrarPlan() {
            //GYMEntities context = new GYMEntities();
            //this.lista = context.Categoria.ToList();
            categoria = 0;
        }

        


    }
}
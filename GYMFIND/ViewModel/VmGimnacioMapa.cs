using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GYMFIND.Models;

namespace GYMFIND.ViewModel
{
    public class VmGimnacioMapa
    {

        public List<Establecimiento> lista { get; set; }

        public void fill() {

            GYMEntities context = new GYMEntities();

            this.lista = context.Establecimiento.ToList();
        }
    }
}
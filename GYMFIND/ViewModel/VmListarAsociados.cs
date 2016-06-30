using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GYMFIND.Models;
namespace GYMFIND.ViewModel
{
    public class VmListarAsociados
    {

        public List<Asociado> lista { get; set; }


        public void fill() {

            GYMEntities context = new GYMEntities();

            this.lista = context.Asociado.ToList();



        }

    }
}
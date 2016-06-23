using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYMFIND.Models;
using System.ComponentModel.DataAnnotations;

namespace GYMFIND.ViewModel
{
    //[Bind(Exclude = "lista")]
    public class VmRegistrarPlan
    {
        public int? planID { get; set; }

        [Required]
        public String nombre { get; set; }
        [Required]
        public String descripcion { get; set; }
        public double costo { get; set; }
        public int establecimiento { get; set; }
        public int categoria { get; set; }
        
        public List<Categoria> lista  { get; set; }

        public GYMEntities context;

        public VmRegistrarPlan() {
            //context = new GYMEntities();
            //GYMEntities context = new GYMEntities();
            //this.lista = context.Categoria.ToList();
            categoria = 0;
        }

        public void Fill(GYMEntities context,int? planID)
        {
            this.planID = planID;

            if (planID.HasValue) {

                Planes obj = context.Planes.FirstOrDefault(x => x.PlanID == planID);

                this.nombre = obj.Nombre;
                this.descripcion = obj.Descripcion;
                this.costo = obj.Costo;
                this.establecimiento = obj.EstablecimientoID;
                this.categoria = obj.categoriaID;

            }


            lista = context.Categoria.ToList();
        }


    }
}
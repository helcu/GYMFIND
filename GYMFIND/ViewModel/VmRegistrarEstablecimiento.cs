using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GYMFIND.Models;
using System.ComponentModel.DataAnnotations;

namespace GYMFIND.ViewModel
{
    public class VmRegistrarEstablecimiento
    {

        public int? establecimientoID { get; set; }

        [Required]
        public String nombre { get; set; }
        [Required]
        public String direccion { get; set; }
        [Required]
        public String ruc { get; set; }
        [Required]
        public String latitud { get; set; }
        [Required]
        public String longitud { get; set; }
        [Required]
        public String portal { get; set; }
        
        public String imagen { get; set; }

        

        public GYMEntities context;



        public void fill(GYMEntities context, int? establecimientoID)
        {
            this.establecimientoID = establecimientoID;

            if (establecimientoID.HasValue)
            {

                Establecimiento obj = context.Establecimiento.FirstOrDefault(x => x.EstablecimientoID == establecimientoID);

                this.nombre = obj.Nombre;
                this.direccion = obj.Direccion;
                this.ruc = obj.RUC;
                this.latitud = obj.Latitud;
                this.longitud = obj.Longitud;
                this.portal = obj.Portal;
                this.imagen = obj.imagen;
            }


            
        }


    }
}
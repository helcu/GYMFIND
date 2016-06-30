using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GYMFIND.Models;
using System.ComponentModel.DataAnnotations;
namespace GYMFIND.ViewModel
{
    public class ViewModelRegistrarAsociado
    {
        public int? asociadoID { get; set; }

        [Required]
        public String usuario { get; set; }
        [Required]
        public String clave { get; set; }
        //public String rol { get; set; }
        public int establecimientoId { get; set; }
        public String imagen { get; set; }

        public List<Establecimiento> lista { get; set; }

        public GYMEntities context;

       

        public void fill(GYMEntities context, int? asociadoID)
        {
            this.asociadoID = asociadoID;

            if (asociadoID.HasValue)
            {

                Asociado obj = context.Asociado.FirstOrDefault(x => x.AsociadoID == asociadoID);

                this.usuario = obj.Usuario;
                this.clave = obj.Clave;
                this.establecimientoId = obj.EstablecimientoID;
                this.imagen = obj.Foto;
                

            }


            lista = context.Establecimiento.ToList();
        }

    }
}
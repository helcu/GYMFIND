//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GYMFIND.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Establecimiento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Establecimiento()
        {
            this.Asociado = new HashSet<Asociado>();
            this.Planes = new HashSet<Planes>();
        }
    
        public int EstablecimientoID { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string RUC { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string Portal { get; set; }
        public string imagen { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Asociado> Asociado { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Planes> Planes { get; set; }
    }
}

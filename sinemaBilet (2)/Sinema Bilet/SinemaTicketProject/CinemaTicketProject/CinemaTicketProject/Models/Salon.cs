//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CinemaTicketProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Salon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Salon()
        {
            this.FilmSalonSeans = new HashSet<FilmSalonSeans>();
            this.Bilet = new HashSet<Bilet>();
        }
    
        public int SalonId { get; set; }
        public string SalonAdi { get; set; }
        public Nullable<int> SalonKapasite { get; set; }
        public Nullable<int> SalonSiraSayisi { get; set; }
        public string SalonAciklamasi { get; set; }
        public string SalonSlogan { get; set; }
        public string SalonResim { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FilmSalonSeans> FilmSalonSeans { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bilet> Bilet { get; set; }
    }
}
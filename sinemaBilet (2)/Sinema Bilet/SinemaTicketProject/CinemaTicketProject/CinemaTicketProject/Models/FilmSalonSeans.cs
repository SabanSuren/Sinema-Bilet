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
    
    public partial class FilmSalonSeans
    {
        public int FilmSalonSeansId { get; set; }
        public Nullable<int> FilmId { get; set; }
        public Nullable<int> SalonId { get; set; }
        public Nullable<int> SeansId { get; set; }
    
        public virtual Film Film { get; set; }
        public virtual Salon Salon { get; set; }
        public virtual Seans Seans { get; set; }
    }
}

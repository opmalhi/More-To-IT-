//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FYP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class City
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public City()
        {
            this.Hotels = new HashSet<Hotel>();
            this.FamousPlaces = new HashSet<FamousPlace>();
        }
    
        public int City_ID { get; set; }
        public string City_Name { get; set; }
        public string City_Picture1 { get; set; }
        public string City_Picture2 { get; set; }
        public string City_Picture3 { get; set; }
        public string City_Picture4 { get; set; }
        public string City_Details { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hotel> Hotels { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FamousPlace> FamousPlaces { get; set; }
    }
}

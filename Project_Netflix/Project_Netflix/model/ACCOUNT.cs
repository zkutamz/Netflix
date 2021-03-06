//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project_Netflix.model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ACCOUNT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ACCOUNT()
        {
            this.FAVOURITE_MOVIES = new HashSet<FAVOURITE_MOVIES>();
            this.PURCHASEs = new HashSet<PURCHASE>();
        }
    
        public int ID { get; set; }
        public string EMAIL { get; set; }
        public string PASSWORD { get; set; }
        public int TYPE { get; set; }
        public int INFORMATION { get; set; }
        public int ACTIVE { get; set; }
    
        public virtual ACCOUNT_TYPE ACCOUNT_TYPE { get; set; }
        public virtual USER_INFORMATION USER_INFORMATION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FAVOURITE_MOVIES> FAVOURITE_MOVIES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PURCHASE> PURCHASEs { get; set; }
    }
}

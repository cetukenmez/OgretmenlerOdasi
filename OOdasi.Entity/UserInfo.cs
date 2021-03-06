//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OOdasi.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserInfo()
        {
            this.Ilanlars = new HashSet<Ilanlar>();
        }
    
        public int UserId { get; set; }
        public string UserMail { get; set; }
        public string UserPass { get; set; }
        public string UserName { get; set; }
        public string UserLastname { get; set; }
        public Nullable<System.DateTime> UserAge { get; set; }
        public Nullable<int> RoleId { get; set; }
        public Nullable<int> UserSehirId { get; set; }
        public Nullable<int> ilanSayisi { get; set; }
        public string UserPhoto { get; set; }
        public Nullable<System.DateTime> UserUyeDate { get; set; }
        public string UserPhone { get; set; }
        public string UserBio { get; set; }
        public string UserLise { get; set; }
        public string UserUniversite { get; set; }
        public string UserBolum { get; set; }
    
        public virtual Sehirler Sehirler { get; set; }
        public virtual UserRole UserRole { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ilanlar> Ilanlars { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SaglikApplication.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ilac
    {
        public int ID { get; set; }
        public int TC { get; set; }
        public string Ad { get; set; }
        public int Hastalik_ID { get; set; }
    
        public virtual Hasta Hasta { get; set; }
        public virtual Hastalik Hastalik { get; set; }
    }
}
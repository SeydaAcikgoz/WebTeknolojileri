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
    
    public partial class Randevu
    {
        public int ID { get; set; }
        public int TC { get; set; }
        public int Dr_ID { get; set; }
        public int Polklinik_ID { get; set; }
        public System.DateTime Tarih { get; set; }
    
        public virtual Doktor Doktor { get; set; }
        public virtual Hasta Hasta { get; set; }
    }
}

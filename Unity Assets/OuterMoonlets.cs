//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SWNAdmin.Utility
{
    using System;
    using System.Collections.Generic;
    
    public partial class OuterMoonlets
    {
        public int Id { get; set; }
        public int ParentDBID { get; set; }
        public Nullable<double> blackbodyTemp { get; set; }
        public string name { get; set; }
        public Nullable<double> orbitalEccent { get; set; }
        public Nullable<double> orbitalPeriod { get; set; }
        public Nullable<double> orbitalRadius { get; set; }
        public Nullable<int> parentID { get; set; }
        public string parentName { get; set; }
        public Nullable<double> planetRadius { get; set; }
        public Nullable<int> selfID { get; set; }
    
        public virtual Planets Planets { get; set; }
    }
}

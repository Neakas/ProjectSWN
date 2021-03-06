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
    
    public partial class MajorMoons
    {
        public int Id { get; set; }
        public Nullable<int> ParentDBID { get; set; }
        public Nullable<double> RVM { get; set; }
        public Nullable<double> SatelliteSize { get; set; }
        public Nullable<int> SatelliteType { get; set; }
        public Nullable<int> atmCate { get; set; }
        public Nullable<double> atmMass { get; set; }
        public Nullable<double> axialTilt { get; set; }
        public Nullable<int> baseType { get; set; }
        public Nullable<double> blackbodyTemp { get; set; }
        public Nullable<double> dayFaceMod { get; set; }
        public Nullable<double> density { get; set; }
        public Nullable<int> descListing { get; set; }
        public Nullable<double> diameter { get; set; }
        public Nullable<double> gravity { get; set; }
        public string hydCoverage { get; set; }
        public Nullable<int> innerMoonlets { get; set; }
        public Nullable<bool> isResonant { get; set; }
        public Nullable<bool> isTideLocked { get; set; }
        public Nullable<int> majorMoons1 { get; set; }
        public Nullable<double> mass { get; set; }
        public Nullable<int> masterOrderId { get; set; }
        public Nullable<double> moonRadius { get; set; }
        public string name { get; set; }
        public Nullable<double> nightFaceMod { get; set; }
        public Nullable<double> orbitalCycle { get; set; }
        public Nullable<double> orbitalEccent { get; set; }
        public Nullable<double> orbitalPeriod { get; set; }
        public Nullable<double> orbitalRadius { get; set; }
        public Nullable<int> outerMoonlets { get; set; }
        public Nullable<double> parentDiam { get; set; }
        public Nullable<int> parentID { get; set; }
        public string parentName { get; set; }
        public Nullable<bool> retrogradeMotion { get; set; }
        public Nullable<double> rotationalPeriod { get; set; }
        public Nullable<int> selfID { get; set; }
        public Nullable<double> siderealPeriod { get; set; }
        public Nullable<double> surfaceTemp { get; set; }
        public Nullable<double> tecActivity { get; set; }
        public Nullable<int> tideForce { get; set; }
        public Nullable<double> tideTotal { get; set; }
        public Nullable<double> volActivity { get; set; }
    
        public virtual Planets Planets { get; set; }
    }
}

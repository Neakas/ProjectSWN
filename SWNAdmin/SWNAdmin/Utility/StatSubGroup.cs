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
    
    public partial class StatSubGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
    
        public virtual StatGroup StatGroup { get; set; }
    }
}

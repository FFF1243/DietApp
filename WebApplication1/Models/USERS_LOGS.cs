//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class USERS_LOGS
    {
        public int USER_ID { get; set; }
        public System.DateTime LOG_DATE { get; set; }
        public string DEVICE { get; set; }
        public int USER_LOG_ID { get; set; }
    
        public virtual USER USER { get; set; }
    }
}

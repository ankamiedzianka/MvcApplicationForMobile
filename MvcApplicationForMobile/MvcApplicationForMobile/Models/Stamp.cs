using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplicationForMobile.Models
{
    public class Stamp
    {
        public bool? IsDeleted {get; set;}
        public DateTime? DateAdded {get; set;}
        public DateTime? DateModified {get; set;}
    }
}
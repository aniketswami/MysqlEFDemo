using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parinay.Models
{
    public class student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public int isActive { get; set; }

    }
}
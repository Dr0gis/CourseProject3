using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string DateTime { get; set; }
        public string Description { get; set; }
        public Organization Organization { get; set; }
    }
}
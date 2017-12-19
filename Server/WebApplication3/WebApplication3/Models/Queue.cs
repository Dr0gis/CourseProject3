using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Queue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Event Event { get; set; }
        public string Description { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Administrator
    {
        [Key]
        public string Id { get; set; }
        public ApplicationUser User { get; set; }
        public Organization Organization { get; set; }
    }
}
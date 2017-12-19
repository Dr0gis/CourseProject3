using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Result
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public CardRFID CardRfid { get; set; }
        public Queue Queue { get; set; }
        public string DateTimeRegistration { get; set; }
        public string DateSuccess { get; set; }
    }
}
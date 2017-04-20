using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    public enum status
    {
        p,
        C,
        R
    }

    public class MyOrderVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public status Status { get; set; }
    }
}
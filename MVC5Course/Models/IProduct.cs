using System.ComponentModel.DataAnnotations;

namespace MVC5Course.Models
{
    public interface IProduct
    {
        string ProductName { get; set; }
        decimal? Price { get; set; }
        bool? Active { get; set; }       
        decimal? Stock { get; set; }
    }
}
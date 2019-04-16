using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TalkAspNetCoreDemo.Models.ViewModels
{
    public class Price
    {
        [Display(Name = "Prezzo")]
        public Money FullPrice { get; set; }

        [Display(Name = "Prezzo Scontato")]
        public Money DiscountPrice { get; set; }

        //public override string ToString()
        //{
        //    return $"{Currency} {Amount:#.00}";
        //}
    }
}

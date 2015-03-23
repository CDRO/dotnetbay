using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DotNetBay.WebApp.Models
{
    public class NewAuctionViewModel
    {
        [Required(ErrorMessage = "Title ist required")]
        [StringLength(1,ErrorMessage="Title must have a minimum length")]
        public string Titel { get; set; }

        [DataType(DataType.Currency, ErrorMessage = "Start price has not a valid format")]
        [Required(ErrorMessage = "Start price is required")]
        public double StartPrice { get; set; }
        
        [StringLength(1,ErrorMessage="Minimum length required")]
        public string Description { get; set; }

        [FutureDateValidator(ErrorMessage = "Start Date must be in the future")]
        [DataType(DataType.DateTime,ErrorMessage = "Not a valid format")]
        [Required(ErrorMessage =  "Start Time is required")]
        public DateTime StartDateTimeUtc { get; set; }

        [FutureDateValidator(ErrorMessage = "End Date must be in the future")]
        [DataType(DataType.DateTime, ErrorMessage = "Not a valid format")]
        [Required(ErrorMessage = "End Time is required")]
        public DateTime EndDateTimeUtc { get; set; }

        public byte[] Image { get; set; }

    }
}
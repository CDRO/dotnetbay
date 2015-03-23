using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotNetBay.WebApp.Models
{
    public class FutureDateValidatorAttribute : ValidationAttribute
    {
        public override bool IsValid(object o)
        {
            DateTime? date = o as DateTime?;
            if (date == null)
            {
                return false;
            }
            return date >DateTime.Now;
        }
    }
}
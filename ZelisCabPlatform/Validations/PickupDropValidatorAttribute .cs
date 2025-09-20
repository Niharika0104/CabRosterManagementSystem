using System.ComponentModel.DataAnnotations;
using ZelisCabPlatform.Models;
using ZelisCabPortalCoreLayer.Models;

namespace ZelisCabPlatform.Validations
{
    public class PickupDropValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var rosterInfo = (RosterInfo)validationContext.ObjectInstance;

            if (rosterInfo.shift >= 1 && rosterInfo.shift <= 3)
            {
                if (string.IsNullOrWhiteSpace(value?.ToString()))
                {
                    return new ValidationResult("This field is required for shifts 1, 2, and 3.");
                }
                
            }
           
            return ValidationResult.Success;
        }
    }

}

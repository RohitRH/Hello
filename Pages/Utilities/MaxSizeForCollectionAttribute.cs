using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApi.Utilities
{
    public class MaxSizeForCollectionAttribute : ValidationAttribute
    {
        private int maxSize;
        public MaxSizeForCollectionAttribute(int _maxSize)
        {
            maxSize = _maxSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var files = value as IFormFileCollection;
            if (files == null)
            {
                return new ValidationResult("Please select files");
            }
           
                foreach (var file in files)
                {
                    if (file.Length > maxSize)
                    {
                        return new ValidationResult("Max file size allowed is 5Mb " + file.FileName + " file size is " + file.Length / 1024 / 1025 + " Mb");
                    }

                    if (file.Length == 0)
                    {
                        return new ValidationResult("File Size Cannot be Zero " + file.FileName);
                    }
                }
            
            
          

            return ValidationResult.Success;
        }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApi.Utilities
{
    public class AllowedExtensionsForCollectionAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensionsForCollectionAttribute(string[] extensions)
        {
            _extensions = extensions;
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
                
                if (file != null)
                {
                    var extension = Path.GetExtension(file.FileName);
                    if (!_extensions.Contains(extension.ToLower()))
                    {
                        return new ValidationResult("Allowed Extensions are pdf,doc,docx Inavalid for " + file.FileName);
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}

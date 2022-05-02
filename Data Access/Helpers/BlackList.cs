using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Helpers
{
    public class BlacklistAttribute : ValidationAttribute
    {
        // Store the words to not allow
        public string[] BlacklistedWords;

        public BlacklistAttribute(string Words)
        {
            BlacklistedWords = Words.Split(',').ToArray();
        }

        // Override the IsValid property to check the value
        public override bool IsValid(object value)
        {
            // Get the content
            var content = value as String;
            // If the content exists, check if it contains any of the blacklisted values
            if (content != null)
            {
                // Return true if it doesn't contain any of the blacklisted words
                return !BlacklistedWords.Any(w => content.ToLower().Contains(w.ToLower()));
            }
            // Otherwise allow null values (optional)
            return true;
        }
    }
}

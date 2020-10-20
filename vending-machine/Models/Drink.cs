using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vending_machine.Models
{
    public class Drink : IValidatableObject
    {
        
        [Key]
        public int  DrinkId { get; set; }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "Decimal(8,1)")]
        [Range(1, 10000, ErrorMessage = "The price must be between {1} and {2}")]
        public decimal Price { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public float Stock { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Price <= 0)
            {
                yield return new
                       ValidationResult("The price must be superior then 0",
                       new[]
                       { nameof(this.Price) }
                       );

            }
            if (this.Stock <= 0)
            {
                yield return new
                       ValidationResult("Stock must be superior then 0",
                       new[]
                       { nameof(this.Stock) }
                       );

            }
        }
    }
}

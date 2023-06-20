using System.ComponentModel.DataAnnotations;

namespace ChefsNDishes.Models;

public class Chef
{
    [Key]
    public int ChefId { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    [ValidateDate]
    public DateTime Birthday { get; set; }


    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public List<Dish> CreatedDishes { get; set; } = new List<Dish>();
}
public class ValidateDate : ValidationAttribute
{
    protected override ValidationResult IsValid
                     (object date, ValidationContext validationContext)
    {
        return ((DateTime)date <= DateTime.Now && (DateTime)date <= DateTime.Now.AddYears(-18))
            ? ValidationResult.Success
            : new ValidationResult("Invalid date range");
    }
}
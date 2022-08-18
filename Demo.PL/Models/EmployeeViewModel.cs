using Demo.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required!")]
        [MaxLength(50, ErrorMessage = "Max Length Of Name Is 50 Chars")]
        [MinLength(5, ErrorMessage = "min Length Of Name Is 5 Chars")]
        public string Name { get; set; }
        [Range(22, 60, ErrorMessage = "Age Must be between 22 and 60")]
        public int? Age { get; set; }
        [RegularExpression(@"^[0-9]{1,10}-[a-zA-Z]{1-40}-[a-zA-Z]{1-40}-[a-zA-Z]{1-40}$"
           , ErrorMessage = "Address Must be Like '123-Street-Region-City'")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        [Range(4000, 8000, ErrorMessage = "Salary Must be between 4000 and 8000")]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }

        public IFormFile Image { get; set; }
        public string ImageName { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}

using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Code Is Required")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Name Length Must Be Between 5 And 100 Char")]
        public string Name { get; set; }

        public DateTime DateOfCreation { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}

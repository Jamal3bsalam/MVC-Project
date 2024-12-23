﻿using Company.G05.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC_03.ViewModels
{
    public class EmployeeViewModel
    {
         public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }
        [Range(25, 60, ErrorMessage = "Age Must Be Between 25 and 60")]
        public int? Age { get; set; }
        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$", ErrorMessage = "Address Must Be Like 123-Street-City-Country")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Salary Is Required")]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
        public int? WorkForId { get; set; }
        public Department? WorkFor { get; set; } // Navigational Property
        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }
    }
}

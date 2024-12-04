using Company.G05.DAL.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC_03.ViewModels
{
    public class DepartmentViewModel
    {
         public int Id { get; set; }
        [Required(ErrorMessage = "Code Is Requierd!!")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name Is Requierd!!")]
        public string Name { get; set; }
        [DisplayName("Date Of Creation")]
        public DateTime DateOfCreation { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}

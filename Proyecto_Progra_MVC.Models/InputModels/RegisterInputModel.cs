using Microsoft.AspNetCore.Mvc;
using Proyecto_Progra_MVC.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Models.InputModels
{
    public class RegisterInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string Lastname { get; set; }

        [Required]
        public int Age { get; set; }

        [Display(Name = "Gender", GroupName = "genero")]
        public Generos Genero { get; set; }

        [Required]
        [Display(Name = "Physical Activity")]
        public string PhysicalActivity { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Remote(action: "checkemail", controller: "auth", ErrorMessage = "Email already exist")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password didn't match")]
        [Display(Name = "Password Confirmation")]
        public string ConfirmPassword { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PatientLibrary
{
    public class Patient
    {
        public int PatientId { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        [RegularExpression("[A-Za-z ]{3,50}", ErrorMessage = "Name Not Valid")]
        public string Name { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "Age is required.")]
        [Range(0, 150, ErrorMessage = "Age Not valid")]
        public int Age { get; set; }
        [Display(Name = "Patient In")]
        public bool In_or_Out { get; set; }
        public bool Deleted { get; set; }
        [Display(Name = "Consultant Doctor")]
        [Required(ErrorMessage = "Doctor's Name is required.")]
        public string Doctor { get; set; }

    }
}

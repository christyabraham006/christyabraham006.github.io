using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Models
{
    public class DoctorList
    {
        public SelectList Doctors= new SelectList(new[] 
        {
            "Dr.Sarath (Anaesthesiology and Critical Care Medicine)",
            "Dr.Kishore (Dermatology) ",
            "Dr.Daniel (Emergency Medicine)",
            "Dr.Bharath(Emergency Medicine)",
            "Dr.Jacob(General Surgery)"
        });
    }
}
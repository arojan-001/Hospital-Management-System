using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace HMS.Models.ViewModels
{
    public class AdminViewModels
    {
        public Patients Patients { get; set; }

        public Nurses Nurses { get; set; }

        public Room Room { get; set; }

        public OnCall OnCall { get; set; }

        public Usage Usage { get; set; }

        public Doctors Doctors { get; set; }

        public Appointments Appointments { get; set; }

        public Medication Medication { get; set; }

        public Perscribtion Perscribtion { get; set; }

        public Department Department { get; set; }

        public Affiliated Affiliated { get; set; }

        public Procedure Procedure { get; set; }

        public Treatment Treatment { get; set; }

        public Undergoes Undergoes { get; set; }

        //Dropdown Lists
        public IEnumerable<SelectListItem> PatientsList { get; set; }

        public IEnumerable<SelectListItem> NursesList { get; set; }

        public IEnumerable<SelectListItem> RoomsList { get; set; }

        public IEnumerable<SelectListItem> UsageList { get; set; }

        public IEnumerable<SelectListItem> DoctorsList { get; set; }

        public IEnumerable<SelectListItem> AppointmentsList { get; set; }

        public IEnumerable<SelectListItem> MedicationsList { get; set; }

        public IEnumerable<SelectListItem> DepartmentsList { get; set; }

        public IEnumerable<SelectListItem> ProceduresList { get; set; }
    }
}

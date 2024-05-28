using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HMS.Models
{
    public class Appointments
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Date & Time")]
        public DateTime DTime { get; set; }

        [Required]
        [Display(Name = "Doctor ID")]
        public int DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public Doctors Doctors { get; set; }

        [Required]
        [Display(Name = "Patient ID")]
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patients Patients { get; set; }
    }
}

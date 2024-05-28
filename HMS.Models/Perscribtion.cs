using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HMS.Models
{
    public class Perscribtion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Details { get; set; }

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

        [Required]
        [Display(Name = "Medication ID")]
        public int MedicationId { get; set; }

        [ForeignKey("MedicationId")]
        public Medication Medication { get; set; }
    }
}

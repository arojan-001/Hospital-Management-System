using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HMS.Models
{
    public class Undergoes
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Patient ID")]
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patients Patients { get; set; }

        [Required]
        [Display(Name = "Doctor ID")]
        public int DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public Doctors Doctors { get; set; }

        [Required]
        [Display(Name = "Nurse ID")]
        public int NurseId { get; set; }

        [ForeignKey("NurseId")]
        public Nurses Nurses { get; set; }

        [Display(Name = "Room Usage ID")]
        public int? UsageId { get; set; }

        [ForeignKey("UsageId")]
        public Usage Usage { get; set; }

        [Required]
        [Display(Name = "Procedure ID")]
        public int ProcedureId { get; set; }

        [ForeignKey("ProcedureId")]
        public Procedure Procedure { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}

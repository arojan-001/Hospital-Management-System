using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HMS.Models
{
    public class Treatment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Doctor ID")]
        public int DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public Doctors Doctors { get; set; }

        [Display(Name = "Procedure ID")]
        public int? ProcedureId { get; set; }

        [ForeignKey("ProcedureId")]
        public Procedure Procedure { get; set; }
    }
}

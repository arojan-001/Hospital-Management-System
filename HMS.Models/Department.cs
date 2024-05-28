using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HMS.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Doctor ID")]
        public int DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public Doctors Doctors { get; set; }
    }
}

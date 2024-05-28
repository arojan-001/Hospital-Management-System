using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HMS.Models
{
    public class Usage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Room ID")]
        public int RoomId { get; set; }

        [ForeignKey("RoomId")]
        public Room Room { get; set; }

        [Required]
        [Display(Name = "PatientId")]
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patients Patients { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime SDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime EDate { get; set; }
    }
}

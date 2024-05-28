using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HMS.Models
{
    public class OnCall
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Room ID")]
        public int RoomId { get; set; }

        [ForeignKey("RoomId")]
        public Room Room { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        public DateTime STime { get; set; }

        [Required]
        [Display(Name = "End Time")]
        public DateTime ETime { get; set; }

        [Required]
        [Display(Name = "Nurse ID")]
        public int NurseId { get; set; }

        [ForeignKey("NurseId")]
        public Nurses Nurses { get; set; }
    }
}

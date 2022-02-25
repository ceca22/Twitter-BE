using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace TwitterApp.Domain.Models
{
    public class UserShare
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get; set;
        }
        public int UserId { get; set; }
        public User User { get; set; }


        public int ShareId { get; set; }
        public Share Share { get; set; }
    }
} 

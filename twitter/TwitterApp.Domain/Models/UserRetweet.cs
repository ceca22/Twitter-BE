﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace TwitterApp.Domain.Models
{
    public class UserRetweet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get; set;
        }

        public int UserId { get; set; }
        public User User { get; set; }


        public int RetweetId { get; set; }
        public Retweet Retweet { get; set; }
    }
} 

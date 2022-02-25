using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TwitterApp.Domain.Models
{
    public class Image
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public byte[] ImageContent { get; set; }

        public ICollection<PostDetails> PostDetails { get; set; } = new List<PostDetails>();


    }
}

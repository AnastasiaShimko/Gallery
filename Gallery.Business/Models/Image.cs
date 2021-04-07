using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gallery.Business.Models
{
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Format { get; set; }
        public byte[] Data { get; set; }
        public ICollection<Category> Categories { get; set; }
        public Image()
        {
            Categories = new List<Category>();
        }
    }
}
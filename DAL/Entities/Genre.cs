using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entities
{
    public class Genre
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}

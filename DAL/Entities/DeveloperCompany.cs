using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
    public class DeveloperCompany
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        [ForeignKey("Country")]
        public string CountryId { get; set; }

        public Country Country { get; set; }
        public UserProfile User { get; set; }
    }
}

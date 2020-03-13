using DAL.IdentityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
    public class UserProfile
    {
        [Key, ForeignKey("User")]
        public string Id { get; set; }
        [Required, StringLength(75)]
        public string Name { get; set; }
        /// <summary>
        /// Фото користувача
        /// </summary>
        [StringLength(150)]
        public string Image { get; set; }
        /// <summary>
        /// Дата реєстрації
        /// </summary>
        public DateTime RegistrationDate { get; set; }
        public virtual DbUser User { get; set; }
    }
}

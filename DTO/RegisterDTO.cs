using System.ComponentModel.DataAnnotations;

namespace TodolistApi.DTO
{
    public class RegisterDTO
    {
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(70)]
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace TodolistApi.DTO
{
    public class LoginDTO
    {
        [StringLength(100)]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

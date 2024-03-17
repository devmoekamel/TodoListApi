using Microsoft.AspNetCore.Identity;

namespace todlistApi.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Taskk> Tasks { get; set; }

    }
}
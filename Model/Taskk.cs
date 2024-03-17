using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace todlistApi.Model
{
    public class Taskk
    {
        public int Id { get; set; }
        [MaxLength(150)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
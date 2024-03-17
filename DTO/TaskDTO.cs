using System.ComponentModel.DataAnnotations.Schema;

namespace TodolistApi.DTO
{
    public class TaskDTO
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted{ get; set; } = false;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string UserId { get; set; }
    }
}

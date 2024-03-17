using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using todlistApi.Model;
using TodolistApi.Context;
using TodolistApi.DTO;
using TodolistApi.Services.TaskService;

namespace TodolistApi.Services.TaskkService
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDBContext _context;

        public TaskService(ApplicationDBContext Context)
        {
            _context = Context;
        }
        public bool AddTask(TaskDTO model)
        {
            var newtask = new Taskk
            {
                Name = model.Name,
                Description = model.Description,
                IsCompleted = model.IsCompleted,
                UserId = model.UserId,
                CreatedOn = model.CreatedOn
            };
            _context.taskks.Add(newtask);
            if(_context.SaveChanges() >1  ) return true;
            else return false;


        }

        public List<Taskk> GetAllTasks([FromBody] string userid)
        {
            var tasks =  _context.taskks.Where(t => t.UserId == userid).ToList();
            return tasks.ToList();
        }

        public bool RemoveTask(int Id)
        {

           var task = _context.taskks.FirstOrDefault(t => t.Id == Id);
            _context.taskks.Remove(task);
            if (_context.SaveChanges() > 1) return true;
            else return false;

        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todlistApi.Model;
using TodolistApi.Context;
using TodolistApi.DTO;
using TodolistApi.Services.TaskService;

namespace TodolistApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer",Roles ="Admin,User")]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskkController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskkController(ITaskService taskService)
        {
            this._taskService = taskService;
        }

        [HttpGet]
        public IActionResult GetTasks([FromBody]string userid)
        {
            var tasks = _taskService.GetAllTasks(userid);
            return Ok(tasks);
        }
        [HttpPost]
        public IActionResult AddTask(TaskDTO task) 
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
           var result = _taskService.AddTask(task);
            if(!result) return Ok(new { status = "success", result = "Task has been Created" });
            return Ok(new {status="success",result="Task has been Created"});
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteTask([FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _taskService.RemoveTask(id);

            if (!result) return Ok("Deleted succecfully");
            else return BadRequest("wen wrong");
            
        }
    }
}

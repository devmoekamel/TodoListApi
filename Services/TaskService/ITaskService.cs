using Microsoft.AspNetCore.Mvc;
using todlistApi.Model;
using TodolistApi.DTO;

namespace TodolistApi.Services.TaskService
{
    public interface ITaskService
    {
        List<Taskk> GetAllTasks([FromBody] string userid);
        bool AddTask(TaskDTO task);
        bool RemoveTask(int Id);


    }
}

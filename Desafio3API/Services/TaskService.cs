using Desafio3API.Models;
using Desafio3API.Repositories;

namespace Desafio3API.Services;

public class TaskService
{
    private readonly TaskRepository _taskRepository;

    public TaskService(TaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<List<TaskItem>> GetAllTasks()
    {
        return await _taskRepository.GetAllTasks();
    }

    public async Task<TaskItem?> GetTaskById(int id)
    {
        return await _taskRepository.GetTaskById(id);
    }

    public async Task<TaskItem> CreateTask(TaskItem task)
    {
        return await _taskRepository.CreateTask(task);
    }

    public async Task<TaskItem?> UpdateTask(int id, TaskItem updatedTask)
    {
        return await _taskRepository.UpdateTask(id, updatedTask);
    }

    public async Task<bool> DeleteTask(int id)
    {
        return await _taskRepository.DeleteTask(id);
    }
    
    public async Task<List<TaskItem>> GetTasksByUserId(int userId)
    {
        return await _taskRepository.GetTasksByUserId(userId);
    }
}
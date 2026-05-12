using Desafio3API.Data;
using Desafio3API.Models;
using Microsoft.EntityFrameworkCore;

namespace Desafio3API.Repositories;

public class TaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskItem>> GetAllTasks()
    {
        return await _context.Tasks.ToListAsync();
    }

    public async Task<TaskItem?> GetTaskById(int id)
    {
        return await _context.Tasks.FindAsync(id);
    }

    public async Task<TaskItem> CreateTask(TaskItem task)
    {
        _context.Tasks.Add(task);

        await _context.SaveChangesAsync();

        return task;
    }

    public async Task<TaskItem?> UpdateTask(int id, TaskItem updatedTask)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task == null)
        {
            return null;
        }

        task.Title = updatedTask.Title;
        task.Description = updatedTask.Description;
        task.IsCompleted = updatedTask.IsCompleted;

        await _context.SaveChangesAsync();

        return task;
    }

    public async Task<bool> DeleteTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task == null)
        {
            return false;
        }

        _context.Tasks.Remove(task);

        await _context.SaveChangesAsync();

        return true;
    }
    
    public async Task<List<TaskItem>> GetTasksByUserId(int userId)
    {
        return await _context.Tasks
            .Where(task => task.UserId == userId)
            .ToListAsync();
    }
}
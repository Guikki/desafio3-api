using Desafio3API.DTOs;
using Desafio3API.Models;
using Desafio3API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Desafio3API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TaskController : ControllerBase
{
    private readonly TaskService _taskService;

    public TaskController(TaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
        {
            return Unauthorized("Usuário não autenticado.");
        }

        var userId = int.Parse(userIdClaim.Value);

        var tasks = await _taskService.GetTasksByUserId(userId);

        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItem>> GetTaskById(int id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
        {
            return Unauthorized("Usuário não autenticado.");
        }

        var userId = int.Parse(userIdClaim.Value);

        var task = await _taskService.GetTaskById(id);

        if (task == null || task.UserId != userId)
        {
            return NotFound("Tarefa não encontrada.");
        }

        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<TaskItem>> CreateTask(CreateTaskDto createTaskDto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
        {
            return Unauthorized("Usuário não autenticado.");
        }

        var userId = int.Parse(userIdClaim.Value);

        var task = new TaskItem
        {
            Title = createTaskDto.Title,
            Description = createTaskDto.Description,
            IsCompleted = createTaskDto.IsCompleted,
            UserId = userId
        };

        var createdTask = await _taskService.CreateTask(task);

        return CreatedAtAction(
            nameof(GetTaskById),
            new { id = createdTask.Id },
            createdTask
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, UpdateTaskDto updateTaskDto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
        {
            return Unauthorized("Usuário não autenticado.");
        }

        var userId = int.Parse(userIdClaim.Value);

        var existingTask = await _taskService.GetTaskById(id);

        if (existingTask == null || existingTask.UserId != userId)
        {
            return NotFound("Tarefa não encontrada.");
        }

        var updatedTask = new TaskItem
        {
            Title = updateTaskDto.Title,
            Description = updateTaskDto.Description,
            IsCompleted = updateTaskDto.IsCompleted,
            UserId = userId
        };

        await _taskService.UpdateTask(id, updatedTask);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var deleted = await _taskService.DeleteTask(id);

        if (!deleted)
        {
            return NotFound("Tarefa não encontrada.");
        }

        return NoContent();
    }
}
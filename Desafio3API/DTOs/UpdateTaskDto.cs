using System.ComponentModel.DataAnnotations;

namespace Desafio3API.DTOs;

public class UpdateTaskDto
{
    [Required(ErrorMessage = "O título é obrigatório.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    public string Description { get; set; } = string.Empty;

    public bool IsCompleted { get; set; } = false;
}
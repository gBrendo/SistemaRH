// Controllers/FuncionariosController.cs
using Microsoft.AspNetCore.Mvc;
using SistemaRH.DTOs;
using SistemaRH.Services;

namespace SistemaRH.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FuncionariosController : ControllerBase
{
    private readonly FuncionarioService _service;

    public FuncionariosController(FuncionarioService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var funcionarios = await _service.ObterTodosAsync();
        return Ok(funcionarios);
    }

    // GET com filtros: /api/funcionarios/filtrar?nome=João&departamentoId=1
    [HttpGet("filtrar")]
    public async Task<IActionResult> Filtrar(
        [FromQuery] string? nome,
        [FromQuery] int? departamentoId)
    {
        var funcionarios = await _service.FiltrarAsync(nome, departamentoId);
        return Ok(funcionarios);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var funcionario = await _service.ObterPorIdAsync(id);
        if (funcionario == null) return NotFound("Funcionário não encontrado.");
        return Ok(funcionario);
    }

    [HttpPost]
    public async Task<IActionResult> Criar(FuncionarioInputDTO dto)
    {
        try
        {
            var novo = await _service.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = novo.Id }, novo);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, FuncionarioInputDTO dto)
    {
        var atualizado = await _service.AtualizarAsync(id, dto);
        if (atualizado == null) return NotFound("Funcionário não encontrado.");
        return Ok(atualizado);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var resultado = await _service.DeletarAsync(id);
        if (!resultado) return NotFound("Funcionário não encontrado.");
        return NoContent();
    }
}
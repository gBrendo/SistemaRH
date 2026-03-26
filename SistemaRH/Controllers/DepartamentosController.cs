// Controllers/DepartamentosController.cs
using Microsoft.AspNetCore.Mvc;
using SistemaRH.Models;
using SistemaRH.Services;

namespace SistemaRH.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartamentosController : ControllerBase
{
    private readonly DepartamentoService _service;

    public DepartamentosController(DepartamentoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var departamentos = await _service.ObterTodosAsync();
        return Ok(departamentos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var departamento = await _service.ObterPorIdAsync(id);
        if (departamento == null) return NotFound("Departamento não encontrado.");
        return Ok(departamento);
    }

    [HttpPost]
    public async Task<IActionResult> Criar(Departamento departamento)
    {
        try
        {
            var novo = await _service.CriarAsync(departamento);
            return CreatedAtAction(nameof(ObterPorId), new { id = novo.Id }, novo);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, Departamento departamento)
    {
        var atualizado = await _service.AtualizarAsync(id, departamento);
        if (atualizado == null) return NotFound("Departamento não encontrado.");
        return Ok(atualizado);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var resultado = await _service.DeletarAsync(id);
        if (!resultado) return NotFound("Departamento não encontrado.");
        return NoContent();
    }
}
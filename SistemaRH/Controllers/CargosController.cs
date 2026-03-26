// Controllers/CargosController.cs
using Microsoft.AspNetCore.Mvc;
using SistemaRH.Models;
using SistemaRH.Services;

namespace SistemaRH.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CargosController : ControllerBase
{
    private readonly CargoService _service;

    public CargosController(CargoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var cargos = await _service.ObterTodosAsync();
        return Ok(cargos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var cargo = await _service.ObterPorIdAsync(id);
        if (cargo == null) return NotFound("Cargo não encontrado.");
        return Ok(cargo);
    }

    [HttpPost]
    public async Task<IActionResult> Criar(Cargo cargo)
    {
        try
        {
            var novo = await _service.CriarAsync(cargo);
            return CreatedAtAction(nameof(ObterPorId), new { id = novo.Id }, novo);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, Cargo cargo)
    {
        var atualizado = await _service.AtualizarAsync(id, cargo);
        if (atualizado == null) return NotFound("Cargo não encontrado.");
        return Ok(atualizado);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var resultado = await _service.DeletarAsync(id);
        if (!resultado) return NotFound("Cargo não encontrado.");
        return NoContent();
    }
}
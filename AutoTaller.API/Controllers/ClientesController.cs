using AutoTaller.Domain.Entities;
using AutoTaller.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace AutoTaller.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ClientesController> _logger;

    public ClientesController(IUnitOfWork unitOfWork, ILogger<ClientesController> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    // GET: api/clientes
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var clientes = await _unitOfWork.Clientes.GetAllAsync();
            return Ok(clientes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener clientes");
            return StatusCode(500, new { message = "Error al obtener clientes" });
        }
    }

    // GET: api/clientes/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var cliente = await _unitOfWork.Clientes.GetByIdAsync(id);
            
            if (cliente == null)
                return NotFound(new { message = $"Cliente con ID {id} no encontrado" });

            return Ok(cliente);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener cliente");
            return StatusCode(500, new { message = "Error al obtener cliente" });
        }
    }

    // POST: api/clientes
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Cliente cliente)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _unitOfWork.Clientes.AddAsync(cliente);
            await _unitOfWork.CommitAsync();

            return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear cliente");
            return StatusCode(500, new { message = "Error al crear cliente" });
        }
    }

    // PUT: api/clientes/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Cliente cliente)
    {
        try
        {
            if (id != cliente.Id)
                return BadRequest(new { message = "El ID no coincide" });

            var existingCliente = await _unitOfWork.Clientes.GetByIdAsync(id);
            if (existingCliente == null)
                return NotFound(new { message = $"Cliente con ID {id} no encontrado" });

            _unitOfWork.Clientes.Update(cliente);
            await _unitOfWork.CommitAsync();

            return Ok(cliente);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar cliente");
            return StatusCode(500, new { message = "Error al actualizar cliente" });
        }
    }

    // DELETE: api/clientes/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var cliente = await _unitOfWork.Clientes.GetByIdAsync(id);
            if (cliente == null)
                return NotFound(new { message = $"Cliente con ID {id} no encontrado" });

            _unitOfWork.Clientes.Delete(cliente);
            await _unitOfWork.CommitAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar cliente");
            return StatusCode(500, new { message = "Error al eliminar cliente" });
        }
    }

    // GET: api/clientes/paginado
    [HttpGet("paginado")]
    public async Task<IActionResult> GetPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var (items, totalCount) = await _unitOfWork.Clientes.GetPagedAsync(pageNumber, pageSize);

            Response.Headers.Add("X-Total-Count", totalCount.ToString());

            return Ok(new
            {
                items,
                pageNumber,
                pageSize,
                totalCount,
                totalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener clientes paginados");
            return StatusCode(500, new { message = "Error al obtener clientes" });
        }
    }
}
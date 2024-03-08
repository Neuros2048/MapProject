using Microsoft.AspNetCore.Mvc;
using Server.Services;
using Shared.DTO;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MapController(TileService tileService) : ControllerBase
{
    
    
    [HttpPost("Add")]
    public async Task<IActionResult> Add(TileDto tileDto)
    {
        
        return Ok(tileDto);
   
    }

    private string slowa = "niewim";


    [HttpGet("get")]
    public async Task<IActionResult> get()
    {
        return Ok(slowa);
   
    }
}
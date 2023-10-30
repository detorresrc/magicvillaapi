using AutoMapper;
using MagicVilla.Data;
using MagicVilla.Models;
using MagicVilla.Models.DTOs;
using MagicVilla.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla.Controllers;

[Route("/api/villas")]
[ApiController]
public class VillaApiController: ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IVillaRepository _villaRepository;

    public VillaApiController(
        IMapper mapper, 
        IVillaRepository villaRepository)
    {
        _mapper = mapper;
        _villaRepository = villaRepository;
    }

    [HttpGet]
    public async Task<ActionResult<VillaDTO[]>> GetVillas()
    {
        return Ok(_mapper.Map<VillaDTO[]>(await _villaRepository.GetAllAsync()));
    }
    
    [HttpGet("{id:int}", Name = "GetVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
    public async Task<ActionResult<Villa>> GetVilla(int id)
    {
        if (id == 0) return BadRequest(new ErrorDto{Status = StatusCodes.Status400BadRequest, Title = "Invalid villa Id"});
        
        var villa = await _villaRepository.GetAsync(v => v.Id == id);
        
        if (villa == null) return NotFound(new ErrorDto{Status = StatusCodes.Status404NotFound, Title = "Villa not found!"});
        
        return Ok(villa);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
    public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody]VillaCreateDTO? villaDto)
    {
        if (villaDto == null) return BadRequest(new ErrorDto{Status = StatusCodes.Status400BadRequest, Title = "Invalid villa data"});
        
        // Manual Validation
        if (VillaStore.villaList.FirstOrDefault(u => u.Name.ToLower() == villaDto.Name.ToLower()) != null)
        {
            ModelState.AddModelError("Name", "Villa name already exists!");
            return BadRequest(ModelState);
        }

        var villa = _mapper.Map<Villa>(villaDto);
        var result = await _villaRepository.CreateAsync(villa);
        if (result == false)
            return BadRequest(new ErrorDto
                { Status = StatusCodes.Status400BadRequest, Title = "Failed saving villa." });

        var responseVillaDto = _mapper.Map<VillaDTO>(villa);
        return CreatedAtRoute("GetVilla", new {id = responseVillaDto.Id}, responseVillaDto);
    }
    
    [HttpDelete("{id:int}", Name = "DeleteVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
    public async Task<IActionResult> DeleteVilla(int id)
    {
        if (id == 0) return BadRequest(new ErrorDto{Status = StatusCodes.Status400BadRequest, Title = "Invalid villa Id"});
        
        var villa = await _villaRepository.GetAsync(v => v.Id == id);

        if (villa == null) return NotFound(new ErrorDto{Status = StatusCodes.Status404NotFound, Title = "Villa not found!"});

        if ((await _villaRepository.RemoveAsync(villa)) == false)
            return BadRequest(new ErrorDto
                { Status = StatusCodes.Status400BadRequest, Title = "Failed saving villa." });
        
        return NoContent();
    }
    
    [HttpPut("{id:int}", Name = "UpdateVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
    public async Task<ActionResult<VillaDTO>> UpdateVilla(int id, [FromBody]VillaUpdateDTO? villa)
    {
        if (villa == null) return BadRequest(new ErrorDto{Status = StatusCodes.Status400BadRequest, Title = "Invalid villa data"});
        
        if (id == 0) return BadRequest(new ErrorDto{Status = StatusCodes.Status400BadRequest, Title = "Invalid villa Id"});
        
        if(villa.Id != id)
            return BadRequest(new ErrorDto{Status = StatusCodes.Status400BadRequest, Title = "Please provide same Id on form request."});
        
        var villaToUpdate = await _villaRepository.GetAsync(v => v.Id == id);

        if (villaToUpdate == null) return NotFound(new ErrorDto{Status = StatusCodes.Status404NotFound, Title = "Villa not found!"});
        
        // Manual Validation
        if (VillaStore.villaList.FirstOrDefault(u => u.Name.ToLower() == villa.Name.ToLower() && u.Id != villa.Id) != null)
        {
            ModelState.AddModelError("Name", "Villa name already exists!");
            return BadRequest(ModelState);
        }

        _mapper.Map(villa, villaToUpdate);
        if ((await _villaRepository.UpdateAsync(villaToUpdate)) == false)
            return BadRequest(new ErrorDto
                { Status = StatusCodes.Status400BadRequest, Title = "Failed updating villa." });
        
        return Ok(_mapper.Map<VillaDTO>(villaToUpdate));
    }
    
    [HttpPatch("{id:int}", Name = "PartiallyUpdateVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
    public async Task<ActionResult<VillaDTO>> PartiallyUpdateVilla(int id, JsonPatchDocument<VillaDTO> villa)
    {
        if (id == 0) return BadRequest(new ErrorDto{Status = StatusCodes.Status400BadRequest, Title = "Invalid villa Id"});
        
        var villaToUpdate = await _villaRepository.GetAsync(v => v.Id == id, false);
        
        var villaToUpdateDto = _mapper.Map<VillaDTO>(villaToUpdate);
        
        if (villaToUpdate == null) return NotFound(new ErrorDto{Status = StatusCodes.Status404NotFound, Title = "Villa not found!"});
        villa.ApplyTo(villaToUpdateDto, ModelState);
        
        if(!ModelState.IsValid) return BadRequest(ModelState);
        
        var newVillaToUpdate = _mapper.Map<Villa>(villaToUpdateDto);
        newVillaToUpdate.CreatedDate = villaToUpdate.CreatedDate;
        newVillaToUpdate.UpdatedDate = DateTime.UtcNow;
        
        if ((await _villaRepository.UpdateAsync(newVillaToUpdate)) == false)
            return BadRequest(new ErrorDto
                { Status = StatusCodes.Status400BadRequest, Title = "Failed updating villa." });
        return Ok(_mapper.Map<VillaDTO>(villaToUpdate));
    }
}
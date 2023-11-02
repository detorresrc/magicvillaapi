using System.Net;
using AutoMapper;
using MagicVilla.Models;
using MagicVilla.Models.DTOs;
using MagicVilla.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.Controllers;

[Route("/api/villas")]
[Authorize]
public class VillaApiController: BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IVillaRepository _villaRepository;
    protected APIResponse _response;
    public VillaApiController(
        IMapper mapper, 
        IVillaRepository villaRepository)
    {
        _mapper = mapper;
        _villaRepository = villaRepository;
        _response = new ();
    }

    [HttpGet]
    public async Task<ActionResult<APIResponse>> GetVillas()
    {
        _response.Result = _mapper.Map<VillaDTO[]>(await _villaRepository.GetAllAsync());
        _response.StatusCode = HttpStatusCode.OK;
        return Ok(_response);
    }
    
    [HttpGet("{id:int}", Name = "GetVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
    public async Task<ActionResult<APIResponse>> GetVilla(int id)
    {
        if (id == 0) return ErrorResponse(HttpStatusCode.BadRequest, "Invalid villa Id");

        var villa = await _villaRepository.GetAsync(v => v.Id == id);

        if (villa == null) return ErrorResponse(HttpStatusCode.NotFound, "Villa not found");

        _response.StatusCode = HttpStatusCode.OK;
        _response.Result = villa;
        
        return Ok(_response);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
    public async Task<ActionResult<APIResponse>> CreateVilla([FromBody]VillaCreateDTO? villaDto)
    {
        if (villaDto == null) return ErrorResponse(HttpStatusCode.BadRequest, "Invalid form data");

        // Manual Validation
        if (await _villaRepository.GetAsync(u => u.Name.ToLower() == villaDto.Name.ToLower()) != null)
            return ErrorResponse(HttpStatusCode.BadRequest, "Villa name already exists");

        var villa = _mapper.Map<Villa>(villaDto);
        var result = await _villaRepository.CreateAsync(villa);
        if (result == false)
            return ErrorResponse(HttpStatusCode.BadRequest, "Failed saving Villa");

        var responseVillaDto = _mapper.Map<VillaDTO>(villa);
        _response.StatusCode = HttpStatusCode.OK;
        _response.Result = responseVillaDto;
        return CreatedAtRoute("GetVilla", new {id = responseVillaDto.Id}, _response);
    }
    
    [HttpDelete("{id:int}", Name = "DeleteVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
    public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
    {
        var villa = await _villaRepository.GetAsync(v => v.Id == id);

        if (villa == null)
            return ErrorResponse(HttpStatusCode.NotFound, "Villa not found");

        if ((await _villaRepository.RemoveAsync(villa)) == false)
            return ErrorResponse(HttpStatusCode.BadRequest, "Failed deleting Villa");
        
        _response.StatusCode = HttpStatusCode.OK;
        _response.Result = "Villa deleted successfully";
        
        return Ok(_response);
    }
    
    [HttpPut("{id:int}", Name = "UpdateVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
    public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody]VillaUpdateDTO? villa)
    {
        if (villa == null) return ErrorResponse(HttpStatusCode.BadRequest, "Invalid form data");
        
        if(villa.Id != id) return ErrorResponse(HttpStatusCode.BadRequest, "Please provide same Id on form request");
        
        var villaToUpdate = await _villaRepository.GetAsync(v => v.Id == id);

        if (villaToUpdate == null) 
            return ErrorResponse(HttpStatusCode.NotFound, "Villa not found");

        _mapper.Map(villa, villaToUpdate);
        if ((await _villaRepository.UpdateAsync(villaToUpdate)) == false)
            return ErrorResponse(HttpStatusCode.BadRequest, "Failed saving Villa");
        
        _response.StatusCode = HttpStatusCode.OK;
        _response.Result = _mapper.Map<VillaDTO>(villaToUpdate);
        
        return Ok(_response);
    }
    
    [HttpPatch("{id:int}", Name = "PartiallyUpdateVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
    public async Task<ActionResult<APIResponse>> PartiallyUpdateVilla(int id, JsonPatchDocument<VillaDTO> villa)
    {
        var villaToUpdate = await _villaRepository.GetAsync(v => v.Id == id, false);
        if (villaToUpdate == null) 
            return ErrorResponse(HttpStatusCode.NotFound, "Villa not found");
        
        var villaToUpdateDto = _mapper.Map<VillaDTO>(villaToUpdate);
        villa.ApplyTo(villaToUpdateDto, ModelState);
        
        var newVillaToUpdate = _mapper.Map<Villa>(villaToUpdateDto);
        newVillaToUpdate.CreatedDate = villaToUpdate.CreatedDate;
        newVillaToUpdate.UpdatedDate = DateTime.UtcNow;

        if ((await _villaRepository.UpdateAsync(newVillaToUpdate)) == false)
            return ErrorResponse(HttpStatusCode.BadRequest, "Failed saving Villa");
        
        _response.StatusCode = HttpStatusCode.OK;
        _response.Result = _mapper.Map<VillaDTO>(villaToUpdate);
        
        return Ok(_response);
    }
}
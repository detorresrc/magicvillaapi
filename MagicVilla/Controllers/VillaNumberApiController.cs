using System.Net;
using AutoMapper;
using MagicVilla.Models;
using MagicVilla.Models.DTOs;
using MagicVilla.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.Controllers;

[Route("/api/villa-numbers")]
public class VillaNumberApiController: BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IVillaNumberRepository _villaNumberRepository;
    private readonly IVillaRepository _villaRepository;
    protected APIResponse _response;
    public VillaNumberApiController(
        IMapper mapper, 
        IVillaNumberRepository villaNumberRepository,
        IVillaRepository villaRepository)
    {
        _mapper = mapper;
        _villaNumberRepository = villaNumberRepository;
        _villaRepository = villaRepository;
        _response = new ();
    }

    [HttpGet]
    public async Task<ActionResult<APIResponseGeneric<VillaNumberDTO[]>>> GetVillaNumbers()
    {
        var response = new APIResponseGeneric<VillaNumberDTO[]>();
        response.Result = _mapper.Map<VillaNumberDTO[]>(await _villaNumberRepository.GetAllAsync());
        response.StatusCode = HttpStatusCode.OK;
        
        return Ok(response);
    }
    
    [HttpGet("{villaNo:int}", Name = "GetVillaNumber")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
    public async Task<ActionResult<APIResponseGeneric<VillaNumber?>>> GetVillaNumber(int villaNo)
    {
        var villa = await _villaNumberRepository.GetAsync(v => v.VillaNo == villaNo);
        if (villa == null)
            return ErrorResponseGeneric<VillaNumber>(HttpStatusCode.NotFound, "Villa Number not found");

        var response = new APIResponseGeneric<VillaNumber>();
        response.StatusCode = HttpStatusCode.OK;
        response.Result = villa;
        
        return Ok(response);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
    public async Task<ActionResult<APIResponseGeneric<VillaNumberDTO>>> CreateVillaNumber([FromBody]VillaNumberCreateDTO? villaDto)
    {
        if (villaDto == null) return ErrorResponseGeneric<VillaNumberDTO>(HttpStatusCode.BadRequest, "Invalid form data");

        if(await _villaRepository.GetAsync(v => v.Id == villaDto.VillaID) == null)
            return ErrorResponseGeneric<VillaNumberDTO>(HttpStatusCode.BadRequest, "Villa not found");
        
        var villa = _mapper.Map<VillaNumber>(villaDto);
        var result = await _villaNumberRepository.CreateAsync(villa);
        if (result == false)
            return ErrorResponseGeneric<VillaNumberDTO>(HttpStatusCode.BadRequest, "Failed saving Villa Number");

        var responseVillaNumberDto = _mapper.Map<VillaNumberDTO>(villa);

        var response = new APIResponseGeneric<VillaNumberDTO>();
        response.StatusCode = HttpStatusCode.OK;
        response.Result = responseVillaNumberDto;
        
        return CreatedAtRoute("GetVillaNumber", new { villaNo = responseVillaNumberDto.VillaNo}, response);
    }
    
    [HttpDelete("{villaNo:int}", Name = "DeleteVillaNumber")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
    public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int villaNo)
    {
        var villa = await _villaNumberRepository.GetAsync(v => v.VillaNo == villaNo);
        if (villa == null)
            return ErrorResponse(HttpStatusCode.NotFound, "Villa Number not found");

        if ((await _villaNumberRepository.RemoveAsync(villa)) == false)
            return ErrorResponse(HttpStatusCode.BadRequest, "Failed deleting Villa Number");
        
        _response.StatusCode = HttpStatusCode.OK;
        _response.Message = "Villa Number deleted successfully";
        
        return Ok(_response);
    }
    
    [HttpPut("{villaNo:int}", Name = "UpdateVillaNumber")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
    public async Task<ActionResult<APIResponseGeneric<VillaNumberDTO>>> UpdateVillaNumber(int villaNo, [FromBody]VillaNumberUpdateDTO? villa)
    {
        if (villa == null) return ErrorResponseGeneric<VillaNumberDTO>(HttpStatusCode.BadRequest, "Invalid form data");
        
        if(villa.VillaNo != villaNo) return ErrorResponseGeneric<VillaNumberDTO>(HttpStatusCode.BadRequest, "Please provide same Id on form request");
        
        var villaToUpdate = await _villaNumberRepository.GetAsync(v => v.VillaNo == villaNo);

        if (villaToUpdate == null) 
            return ErrorResponseGeneric<VillaNumberDTO>(HttpStatusCode.NotFound, "Villa Number not found");

        if(await _villaRepository.GetAsync(v => v.Id == villa.VillaID) == null)
            return ErrorResponseGeneric<VillaNumberDTO>(HttpStatusCode.BadRequest, "Villa not found");
        
        _mapper.Map(villa, villaToUpdate);
        if ((await _villaNumberRepository.UpdateAsync(villaToUpdate)) == false)
            return ErrorResponseGeneric<VillaNumberDTO>(HttpStatusCode.BadRequest, "Failed saving Villa Number");
        
        var response = new APIResponseGeneric<VillaNumberDTO>();
        response.StatusCode = HttpStatusCode.OK;
        response.Result = _mapper.Map<VillaNumberDTO>(villaToUpdate);
        
        return Ok(response);
    }
    
    [HttpPatch("{villaNo:int}", Name = "PartiallyUpdateVillaNumber")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
    public async Task<ActionResult<APIResponseGeneric<VillaNumberDTO>>> PartiallyUpdateVillaNumber(int villaNo, JsonPatchDocument<VillaNumberDTO> villa)
    {
        var villaToUpdate = await _villaNumberRepository.GetAsync(v => v.VillaNo == villaNo, false);
        if (villaToUpdate == null) 
            return ErrorResponseGeneric<VillaNumberDTO>(HttpStatusCode.NotFound, "Villa Number not found");
        
        var villaToUpdateDto = _mapper.Map<VillaNumberDTO>(villaToUpdate);
        villa.ApplyTo(villaToUpdateDto, ModelState);
        
        if(await _villaRepository.GetAsync(v => v.Id == villaToUpdateDto.VillaNo) == null)
            return ErrorResponseGeneric<VillaNumberDTO>(HttpStatusCode.BadRequest, "Villa not found");
        
        var newVillaNumberToUpdate = _mapper.Map<VillaNumber>(villaToUpdateDto);
        newVillaNumberToUpdate.CreatedDate = villaToUpdate.CreatedDate;
        newVillaNumberToUpdate.UpdatedDate = DateTime.UtcNow;

        if ((await _villaNumberRepository.UpdateAsync(newVillaNumberToUpdate)) == false)
            return ErrorResponseGeneric<VillaNumberDTO>(HttpStatusCode.BadRequest, "Failed saving Villa Number");
        
        var response = new APIResponseGeneric<VillaNumberDTO>();
        response.StatusCode = HttpStatusCode.OK;
        response.Result = _mapper.Map<VillaNumberDTO>(villaToUpdate);
        
        return Ok(response);
    }
}
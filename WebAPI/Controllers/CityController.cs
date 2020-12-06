using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Interfaces;
using WebAPI.Dtos;
using System;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {  
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public CityController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        //GET api/city
        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            var cities =await uow.CityRepository.GetCitiesAsync();
            var citiesDto = mapper.Map<IEnumerable<CityDto>>(cities);
            return Ok(citiesDto);
        }      

        //POST api/city/post --post the data in JSON format
        [HttpPost("post")]
        public async Task<IActionResult> AddCity(CityDto cityDto)
        { 
            var city = mapper.Map<City>(cityDto);
            city.LastUpdatedBy = "Mahbub";
            city.LastUpdatedOn = DateTime.Now;           

            uow.CityRepository.AddCity(city);
            await uow.SaveAsync();
            return StatusCode(201);
        }

        //Update Put api/city/update --update the data in JSON format
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCity(int id, CityDto cityDto)
        { 
            if(id != cityDto.Id)
                return BadRequest("Update not allowed");

            var cityFromDb = await uow.CityRepository.FindCity(id);
            if(cityFromDb == null) 
                return BadRequest("Update not allowed!");

            cityFromDb.LastUpdatedBy = "Mahbub";
            cityFromDb.LastUpdatedOn = DateTime.Now;
            mapper.Map(cityDto, cityFromDb);

            //throw new Exception("Some unknown error occured!");
            
            await uow.SaveAsync();
            return StatusCode(200);
        }

        //Update Put api/city/update --update the data in JSON format
        [HttpPut("updateCityName/{id}")]
        public async Task<IActionResult> UpdateCity(int id, CityUpdateDto cityDto)
        { 
            var cityFromDb = await uow.CityRepository.FindCity(id);
            cityFromDb.LastUpdatedBy = "Azad";
            cityFromDb.LastUpdatedOn = DateTime.Now;
            mapper.Map(cityDto, cityFromDb);
            await uow.SaveAsync();
            return StatusCode(200);
        }

        //Update patch api/city/update --update the data in JSON format
        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdateCity(int id, JsonPatchDocument<City> cityToPatch)
        { 
            var cityFromDb = await uow.CityRepository.FindCity(id);
            cityFromDb.LastUpdatedBy = "Azad";
            cityFromDb.LastUpdatedOn = DateTime.Now;

            cityToPatch.ApplyTo(cityFromDb, ModelState);            
            await uow.SaveAsync();
            return StatusCode(200);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {     
            uow.CityRepository.DeletCity(id);     
            await uow.SaveAsync();
            return Ok(id);
        }
        
    }
}
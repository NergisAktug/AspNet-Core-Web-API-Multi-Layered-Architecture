using AutoMapper;
using DotnetNLayerProject.API.DTOs;
using DotnetNLayerProject.Core.Models;
using DotnetNLayerProject.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetNLayerProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IService<Person> _personService;
        private readonly IMapper _mapper;
        public PersonsController(IService<Person> service,IMapper mapper)
        {
            _personService = service;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var persons = await _personService.GetAllAsync();
            return Ok(persons);
        }
        [HttpPost]
        public async Task<IActionResult> Save(PersonDto personDto)
        {
            var newPerson = await _personService.AddAsync(_mapper.Map<Person>(personDto));
            return Ok(_mapper.Map<PersonDto>(newPerson));
        }


    }
}

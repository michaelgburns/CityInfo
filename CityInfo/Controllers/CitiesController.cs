using CityInfo.Models;
using CityInfo.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        private ICityInfoRepository _cityInfoRepository;

        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet]
        public IActionResult GetCities()
        {
            //return Ok(CitiesDataStore.Current.Cities);

            var cityResults = _cityInfoRepository.GetCities();

            var results = new List<CityWithoutPointsOfInterestDto>();

            foreach (var city in cityResults)
            {
                results.Add(new CityWithoutPointsOfInterestDto
                {
                    Id          = city.Id,
                    Name        = city.Name,
                    Description = city.Description
                });
            }

            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }
    }
}

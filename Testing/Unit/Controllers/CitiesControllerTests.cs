using CityInfo.Controllers;
using CityInfo.Models;
using Machine.Specifications;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Unit.Controllers.CitiesControllerTests
{
    [Subject(typeof(CitiesController))]
    public class Context
    {
        public static CitiesController controller;

        public Context()
        {
            controller = new CitiesController();
        }
    }

    #region GetCities

    class when_I_call_GetCities : Context
    {
        static IActionResult result;

        Establish context = () =>
        {

        };

        Because of = () => result = controller.GetCities();

        It returns_a_result_of_type = () => result.ShouldBeOfExactType<OkObjectResult>();
        It returns_a_list_of_cities = () => ((OkObjectResult)result).Value.ShouldBeOfExactType<List<CityDto>>();
        It returns_the_correct_number_of_cities = () =>
        {
            List<CityDto> cities = ((OkObjectResult)result).Value as List<CityDto>;
            cities.Count.ShouldEqual(2);
        };
    }

    #endregion
}

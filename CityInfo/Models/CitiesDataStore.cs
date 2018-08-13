using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Models
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public List<CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
            Cities = new List<CityDto>
            {
                new CityDto
                {
                    Id          = 1,
                    Name        = "Dublin",
                    Description = "Dublin city"
                },

                new CityDto
                {
                    Id          = 2,
                    Name        = "Belfast",
                    Description = "Belfast city"
                }
            };
        }
    }
}

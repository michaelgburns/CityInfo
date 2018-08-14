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
                    Description = "Dublin city",
                    PointsOfInterest = new List<PointOfInterestDto>
                    {
                        new PointOfInterestDto{ Id = 1, Name = "Croke park", Description = "Football field"},
                        new PointOfInterestDto{ Id = 2, Name = "Henry street", Description = "Shopping area"}
                    }
                },

                new CityDto
                {
                    Id          = 2,
                    Name        = "Belfast",
                    Description = "Belfast city",
                    PointsOfInterest = new List<PointOfInterestDto>
                    {
                        new PointOfInterestDto{ Id = 1, Name = "Zoo", Description = "Zoo for wild animals"},
                        new PointOfInterestDto{ Id = 2, Name = "Merchant hotel", Description = "Overpriced hotel"}
                    }
                }
            };
        }
    }
}

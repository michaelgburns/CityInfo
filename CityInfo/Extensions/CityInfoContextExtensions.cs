using CityInfo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Extensions
{
    public static class CityInfoContextExtensions
    {
        public static void EnsureSeedDataForContext(this CityInfoContext context)
        {
            if (context.Cities.Any())
            {
                return;
            }

            var cities  = new List<City>
            {
                new City
                {
                    Name        = "Dublin",
                    Description = "Dublin city",
                    PointsOfInterest = new List<PointOfInterest>
                    {
                        new PointOfInterest{ Name = "Croke park", Description = "Football field"},
                        new PointOfInterest{ Name = "Henry street", Description = "Shopping area"}
                    }
                },

                new City
                {                    
                    Name        = "Belfast",
                    Description = "Belfast city",
                    PointsOfInterest = new List<PointOfInterest>
                    {
                        new PointOfInterest{ Name = "Zoo", Description = "Zoo for wild animals"},
                        new PointOfInterest{ Name = "Merchant hotel", Description = "Overpriced hotel"}
                    }
                }
            };

            context.Cities.AddRange(cities);
            context.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using OdeToFood.core;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
    }
    public class InMemoryRestaurantData : IRestaurantData
    {
        public List<Restaurant> Restaurants { get; set; }
        public InMemoryRestaurantData()
        {
            Restaurants = new List<Restaurant>()
            {
                new Restaurant { Id =1, Name = "Pizza", Location = "MaryLand", Cuisine = CuisineTyoe.Itaian},
                new Restaurant { Id =2, Name = "Marakon", Location = "Rzym", Cuisine = CuisineTyoe.Itaian},
                new Restaurant { Id =3, Name = "La Costa", Location = "California", Cuisine = CuisineTyoe.Mexican}
            };
        }
        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            return Restaurants.OrderBy(r => r.Name).Where(r => string.IsNullOrEmpty(name) || r.Name.StartsWith(name)) ;
        }
    }
}

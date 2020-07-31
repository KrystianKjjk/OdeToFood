using System;
using System.Collections.Generic;
using System.Linq;
using OdeToFood.core;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetRestaurant(int id);
        Restaurant Update(Restaurant updatedRestaurant);
        Restaurant Add(Restaurant newRestaurant);
        int Commit();
    }
    public class InMemoryRestaurantData : IRestaurantData
    {
        public List<Restaurant> Restaurants { get; set; }
        public InMemoryRestaurantData()
        {
            Restaurants = new List<Restaurant>()
            {
                new Restaurant { Id =1, Name = "Pizza", Location = "MaryLand", Cuisine = CuisineType.Itaian},
                new Restaurant { Id =2, Name = "Marakon", Location = "Rzym", Cuisine = CuisineType.Itaian},
                new Restaurant { Id =3, Name = "La Costa", Location = "California", Cuisine = CuisineType.Mexican}
            };
        }
        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            return Restaurants.OrderBy(r => r.Name).Where(r => string.IsNullOrEmpty(name) || r.Name.StartsWith(name)) ;
        }

        public Restaurant GetRestaurant(int id)
        {
            return Restaurants.SingleOrDefault(r => r.Id == id);
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = Restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);
            if(restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }
            return restaurant;
        }

        public int Commit()
        {
            return 0;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            Restaurants.Add(newRestaurant);
            newRestaurant.Id = Restaurants.Max(r => r.Id) + 1;

            return newRestaurant;
        }
    }
}

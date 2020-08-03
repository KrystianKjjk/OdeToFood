using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OdeToFood.core;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext dbContext;

        public SqlRestaurantData(OdeToFoodDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            dbContext.Add(newRestaurant);

            return newRestaurant;
        }

        public int Commit()
        {
            return dbContext.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetRestaurant(id);

            if(restaurant != null)
            {
                dbContext.Restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return dbContext.Restaurants;
        }

        public int GetCountOfRestaurants()
        {
            return dbContext.Restaurants.Count();
        }

        public Restaurant GetRestaurant(int id)
        {
            return dbContext.Restaurants.Find(id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            return dbContext.Restaurants
                .Where(r => string.IsNullOrEmpty(name) || r.Name.StartsWith(name))
                .OrderBy(r => r.Name) ;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var entity = dbContext.Restaurants.Attach(updatedRestaurant);
            entity.State = EntityState.Modified;

            return updatedRestaurant;
        }
    }
}

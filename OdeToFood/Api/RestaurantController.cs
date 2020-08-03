using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OdeToFood.core;
using OdeToFood.Data;

namespace OdeToFood.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantData restaurantData;

        public RestaurantController(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }
        // GET: api/Restaurant
        [HttpGet]
        public IEnumerable<Restaurant> Get()
        {
            return restaurantData.GetAll();
        }

        // GET: api/Restaurant/5
        [HttpGet("{id}", Name = "Get")]
        public  IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var restaurants = restaurantData.GetAll();
            if(restaurants == null)
                return NotFound();

            return Ok(restaurants);
        }

        // POST: api/Restaurant
        [HttpPost]
        public IActionResult Post([FromBody] Restaurant restaurant)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var addedRestaurant = restaurantData.Add(restaurant);
            restaurantData.Commit();

            return CreatedAtAction("GetRestaurant", addedRestaurant);
        }

        // PUT: api/Restaurant/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Restaurant restaurant)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            restaurant.Id = id;

            var updatedRestaurant = restaurantData.Update(restaurant);
            if(updatedRestaurant != null){
                return NotFound();
            }
            restaurantData.Commit();
            return NoContent();

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deletedRestaurant = restaurantData.Delete(id);

            if (deletedRestaurant == null)
                return NotFound();

            restaurantData.Commit();

            return Ok();
        }
    }
}

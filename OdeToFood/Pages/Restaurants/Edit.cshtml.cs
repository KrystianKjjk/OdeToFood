using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        public IHtmlHelper HtmlHelper { get; }

        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel(IRestaurantData restaurantData,
                            IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            HtmlHelper = htmlHelper;
        }
        public IActionResult OnGet(int? restaurantId)
        {
            Cuisines = HtmlHelper.GetEnumSelectList<CuisineType>();

            if(restaurantId.HasValue)
            {
                Restaurant = restaurantData.GetRestaurant(restaurantId.Value);

            }
            else
            {
                Restaurant = new Restaurant();
            }
            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Cuisines = HtmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
            }

            _ = Restaurant.Id > 0 ? restaurantData.Update(Restaurant) : restaurantData.Add(Restaurant);
            restaurantData.Commit();

            TempData["Message"] = "Restaurant saved!";
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });

        }
    }
}

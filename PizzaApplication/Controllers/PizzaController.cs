using Microsoft.AspNetCore.Mvc;
using PizzaApplication.Models;
using System.Collections.Generic;
using System.Linq;

namespace PizzaApp.Controllers
{
    [Route("api/[controller]")] // This sets the base route for the controller (e.g., /api/Pizza)
    [ApiController] // This indicates that this class is an API controller
    public class PizzaController : ControllerBase
    {
        // Create a list of pizzas with 10 options
        private static List<Pizza> Pizzas = new List<Pizza>
        {
            new Pizza { Id = 1, Name = "Margherita", IsGlutenFree = false },
            new Pizza { Id = 2, Name = "Pepperoni", IsGlutenFree = false },
            new Pizza { Id = 3, Name = "Vegetarian", IsGlutenFree = true },
            new Pizza { Id = 4, Name = "Hawaiian", IsGlutenFree = false },
            new Pizza { Id = 5, Name = "Meat Lovers", IsGlutenFree = false },
            new Pizza { Id = 6, Name = "BBQ Chicken", IsGlutenFree = false },
            new Pizza { Id = 7, Name = "Buffalo", IsGlutenFree = false },
            new Pizza { Id = 8, Name = "Supreme", IsGlutenFree = false },
            new Pizza { Id = 9, Name = "Cheese", IsGlutenFree = true },
            new Pizza { Id = 10, Name = "Mushroom", IsGlutenFree = true }
        };

        // GET: api/Pizza
        [HttpGet] // This handles GET requests to /api/Pizza
        public ActionResult<IEnumerable<Pizza>> GetPizzas()
        {
            // Return the list of all pizzas
            return Ok(Pizzas);
        }

        // GET: api/Pizza/5
        [HttpGet("{id}")] // This handles GET requests to /api/Pizza/{id} (e.g., /api/Pizza/5)
        public ActionResult<Pizza> GetPizza(int id)
        {
            // Find the pizza with the matching ID
            var pizza = Pizzas.FirstOrDefault(p => p.Id == id);

            // If the pizza is not found, return a 404 Not Found error
            if (pizza == null)
            {
                return NotFound();
            }

            // Return the pizza
            return Ok(pizza);
        }

        // POST: api/Pizza
        [HttpPost] // This handles POST requests to /api/Pizza
        public ActionResult<Pizza> CreatePizza([FromBody] Pizza pizza)
        {
            // Generate a new ID for the pizza (max ID + 1)
            pizza.Id = Pizzas.Max(p => p.Id) + 1;

            // Add the new pizza to the list
            Pizzas.Add(pizza);

            // Return a 201 Created response with the new pizza
            return CreatedAtAction(nameof(GetPizza), new { id = pizza.Id }, pizza);
        }

        // PUT: api/Pizza/5
        [HttpPut("{id}")] // This handles PUT requests to /api/Pizza/{id} (e.g., /api/Pizza/5)
        public IActionResult UpdatePizza(int id, [FromBody] Pizza updatedPizza)
        {
            // Find the pizza with the matching ID
            var pizza = Pizzas.FirstOrDefault(p => p.Id == id);

            // If the pizza is not found, return a 404 Not Found error
            if (pizza == null)
            {
                return NotFound();
            }

            // Update the pizza's name and gluten-free status
            pizza.Name = updatedPizza.Name;
            pizza.IsGlutenFree = updatedPizza.IsGlutenFree;

            // Return a 204 No Content response (successful update)
            return NoContent();
        }

        // DELETE: api/Pizza/5
        [HttpDelete("{id}")] // This handles DELETE requests to /api/Pizza/{id} (e.g., /api/Pizza/5)
        public IActionResult DeletePizza(int id)
        {
            // Find the pizza with the matching ID
            var pizza = Pizzas.FirstOrDefault(p => p.Id == id);

            // If the pizza is not found, return a 404 Not Found error
            if (pizza == null)
            {
                return NotFound();
            }

            // Remove the pizza from the list
            Pizzas.Remove(pizza);

            // Return a 204 No Content response (successful deletion)
            return NoContent();
        }
    }
}
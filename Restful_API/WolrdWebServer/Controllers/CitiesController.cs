using WolrdWebServer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
namespace WolrdWebServer.Controllers
{
    [Route("api/[controller]")]
    public class CitiesController:Controller
    {
        private WorldDbContext dbContext;
        public CitiesController()
        {
            var connectionString = "server=localhost;port=3306;database=world;userid=root;pwd=Gearsofwar#3;sslmode=none";
            this.dbContext = WorldDbContextFactory.Create(connectionString);
        }

        [HttpGet]
        public ActionResult GetCities()
        {
            return Ok(this.dbContext.City.ToArray());
        }
        [HttpGet("{id}")]
        public ActionResult GetCityByID(int id)
        {
            City target = this.dbContext.City.SingleOrDefault(ct=>ct.ID==id);
            if(target!=null)
            {
                return Ok(target);
            }
            else
            return NotFound();            
        }
        [HttpGet("cc/{cc}")]
        public ActionResult GetCityByCountryCode(string cc)
        {
            var cities = this.dbContext.City.Where(ct=>string.Equals(ct.CountryCode,cc)).ToArray();
            return Ok(cities);
        }

        [HttpPost]
        public ActionResult Post([FromBody]City city)
        {
            if(!this.ModelState.IsValid){
                return BadRequest();
            }
            this.dbContext.City.Add(city);
            this.dbContext.SaveChanges();
            return Created($"api/cities/{city.ID}",city);
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromRoute]int id, [FromBody]City city)
        {
            if(!this.ModelState.IsValid)
            {
                return BadRequest();
            }
            City target = this.dbContext.City.SingleOrDefault(ct=>ct.ID==id);
            if(target !=null)
            {
                this.dbContext.Entry(target).CurrentValues.SetValues(city);
                this.dbContext.SaveChanges();
                return Ok();
            }
            else
            return NotFound();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            City target = this.dbContext.City.SingleOrDefault(ct=>ct.ID==id);
            if(target!=null)
            {
                this.dbContext.City.Remove(target);
                this.dbContext.SaveChanges();
                return Ok();
            }
            else
            return NotFound();
        }
    }
}
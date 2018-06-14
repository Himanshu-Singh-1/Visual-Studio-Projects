using Microsoft.AspNetCore.Mvc;
using WolrdWebServer.Models;
using System.Linq;
namespace WolrdWebServer
{
    [Route("api/[controller]")]
    public class CountriesController:Controller
    {
        private WorldDbContext dbContext;
        public CountriesController()
        {
            var connectionString = "server=localhost;port=3306;database=world;userid=root;pwd=Gearsofwar#3;sslmode=none";
            this.dbContext = WorldDbContextFactory.Create(connectionString); 
        }

        [HttpGet]
        public ActionResult GetCountry()
        {
            return Ok(this.dbContext.Country.ToArray());
        }
    }
}
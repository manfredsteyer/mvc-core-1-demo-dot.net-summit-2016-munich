using FlugDemo.Data;
using FlugDemo.Models;
using Microsoft.AspNet.Mvc;
using SummitDemo.ActionResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SummitDemo.Controllers
{
    [Route("api/[controller]")]
    public class FlugController: Controller
    {
        IFlugRepository repo;

        public FlugController(IFlugRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public List<Flug> GetAll() {
            return this.repo.FindAll();
        }

        [HttpGet("{id}")]
        public Flug GetById(int id) {
            return this.repo.FindById(id);
        }

        [HttpGet("byRoute")]
        public List<Flug> GetByRoute(string von, string nach) {
            return this.repo.FindByRoute(von, nach);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Flug flug) {
            this.repo.Save(flug);
            // Ok()
            // HttpNotFound

            return CreatedAtAction("GetById", new { id = flug.Id  }, flug);
            // return new AcceptedActionResult();
        }
    }
}

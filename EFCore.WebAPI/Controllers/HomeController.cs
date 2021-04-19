using EFCore.Domain;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        public readonly HeroiContext _context;
        public HomeController(HeroiContext context)
        {
            _context = context;
        }

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("filtro/{nome}")]
        public ActionResult GetFiltro(string nome)
        {
            var listHeroi = _context.Herois
                             .Where(h => h.Nome.Contains(nome))
                             .ToList();

            //var listHeroi = (from heroi in _context.Herois
            //                 where heroi.Nome.Contains(nome)
            //                 select heroi).ToList();

            return Ok(listHeroi);
        }

        [HttpGet("Atualizar/{nameHero}")]
        //[HttpGet("{id}")]
        public ActionResult<string> Get(string nameHero)
        //public ActionResult<string> Get(int id)
        {
            //var heroi = new Heroi { Nome = nameHero };

            var heroi = _context.Herois
                             .Where(h => h.Id == 1)
                             .FirstOrDefault();
            heroi.Nome = "Homem Aranha";
            //_context.Herois.Add(heroi);
            _context.SaveChanges();

            return Ok();
            //using (var contexto = new HeroiContext())
            //{
            //    contexto.Herois.Add(heroi);
            //    //contexto.Add(heroi); forma não especificada
            //    contexto.SaveChanges();
            //}
            //return Ok(); // retorna status code = 200
        }

        [HttpGet("AddRange")]
        //[HttpGet("{id}")]
        public ActionResult<string> GetAddRange()
        //public ActionResult<string> Get(int id)
        {
            _context.AddRange(
                new Heroi { Nome = "Capitão América" },
                new Heroi { Nome = "Routor Estranho" },
                new Heroi { Nome = "Pantera Negra" },
                new Heroi { Nome = "Viuva Negra" },
                new Heroi { Nome = "Gavião Arqueiro" },
                new Heroi { Nome = "Hulk" }
            );
            _context.SaveChanges();

            return Ok();
        }

        //Delete
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var heroi = _context.Herois
                .Where(x => x.Id == id)
                .Single();
            _context.Herois.Remove(heroi);
            _context.SaveChanges();
        }

    }
}

using Microsoft.AspNetCore.Mvc;   
using Lager.DAL;                  
using Lager.Models;               

namespace Lager.Controllers
{
    // ber repositoryet om data, og sender riktig visning tilbake.
    public class ProductController : Controller
    {
        // Repositoryet vi får fra dependency injection (registrert i Program.cs)
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // ---- LISTE ----
        // GET /Product/Table: viser alle produkter i en tabell
        public async Task<IActionResult> Table()
        {
            var products = await _productRepository.GetAll();
            if (products == null)
            {
                return NotFound("Fant ingen produkter");
            }
            // Sender lista til Views/Product/Table.cshtml
            return View(products);
        }

        // ---- DETALJER ----
        // GET /Product/Details/3: viser ett produkt
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound("Fant ikke produktet");
            }
            return View(product);
        }

        // ---- OPPRETT ----
        // GET /Product/Create: viser et tomt skjema
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST /Product/Create: tar imot det utfylte skjemaet
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            // ModelState.IsValid sjekker reglene i modellen ([Required], [Range] osv.)
            if (ModelState.IsValid)
            {
                bool ok = await _productRepository.Create(product);
                if (ok)
                {
                    // Tilbake til lista når alt gikk bra
                    return RedirectToAction(nameof(Table));
                }
            }
            // Noe var galt: vis skjemaet på nytt med det brukeren skrev og feilmeldingene
            return View(product);
        }

        // ---- ENDRE ----
        // GET /Product/Update/3: viser skjemaet fylt ut med produktet
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound("Fant ikke produktet");
            }
            return View(product);
        }

        // POST /Product/Update: lagrer endringene
        [HttpPost]
        public async Task<IActionResult> Update(Product product)
        {
            if (ModelState.IsValid)
            {
                bool ok = await _productRepository.Update(product);
                if (ok)
                {
                    return RedirectToAction(nameof(Table));
                }
            }
            return View(product);
        }

        // ---- SLETT ----
        // GET /Product/Delete/3: viser "er du sikker?"-siden
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound("Fant ikke produktet");
            }
            return View(product);
        }

        // POST /Product/DeleteConfirmed: sletter!
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool ok = await _productRepository.Delete(id);
            if (!ok)
            {
                return BadRequest("Sletting feilet");
            }
            return RedirectToAction(nameof(Table));
        }
    }
}
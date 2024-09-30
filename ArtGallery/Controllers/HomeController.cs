using ArtGallery.Data;
using ArtGallery.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Web;

namespace ArtGallery.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public HomeController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["ActiveNav"] = "Home";
            var exhibitions = await _context.Exhibitions.ToListAsync();
            var exhibitionViews = _mapper.Map<List<ExhibitionView>>(exhibitions);
            return View(exhibitionViews);
        }

        [AllowAnonymous]
        [Route("/Forbidden")]
        public IActionResult Forbidden()
        {
            return View();
        }
    }
}

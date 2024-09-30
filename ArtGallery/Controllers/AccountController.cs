using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArtGallery.Data;
using ArtGallery.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace ArtGallery.Controllers
{

    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AccountController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            // Kiểm tra xem tên người dùng đã tồn tại chưa
            var existingAccount = await _context.Accounts
                .FirstOrDefaultAsync(a => a.UserName == register.UserName);

            if (existingAccount != null)
            {
                // Nếu tên người dùng đã tồn tại, thêm lỗi vào ModelState
                ModelState.AddModelError("UserName", "Tên người dùng đã tồn tại.");
                return View(register);
            }

            if (register.Role == "customer")
            {
                var account = _mapper.Map<Account>(register);
                account.Role = "Customer";
                _context.Add(account);
                await _context.SaveChangesAsync();

                var customer = _mapper.Map<Customer>(register);
                customer.AccountId = account.AccountId;
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            else if (register.Role == "artist")
            {
                var account = _mapper.Map<Account>(register);
                account.Role = "Artist";
                _context.Add(account);
                await _context.SaveChangesAsync();

                var artist = _mapper.Map<Artist>(register);
                artist.AccountId = account.AccountId;
                _context.Add(artist);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var account = await _context.Accounts
                                     .FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);

            if (account != null)
            {
                var claims = new List<Claim>
                    {
                        new Claim("AccountId", account.AccountId.ToString()),
                        new Claim(ClaimTypes.Name, account.UserName),
                        new Claim(ClaimTypes.Role, account.Role),
                    };

                var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", account.Role == "Admin" ? "Admin" : "Home");
            }

            return View();
        }

        [HttpGet, HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}

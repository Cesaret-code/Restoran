using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restoran.Areas.Boss.ViewModels.Chef;
using Restoran.DAL;
using Restoran.Helpers.Extentions;
using Restoran.Models;

namespace Restoran.Areas.Boss.Controllers
{
    [Area("Boss")]
    public class ChefController : Controller
    {
        AppDbContext _context;
        IWebHostEnvironment _env;

        public ChefController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }

        public async Task<IActionResult> Index()
        {
            List<Chef> chefs = await _context.Chefs.ToListAsync();
            return View(chefs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ChefVm chefVm)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            if(!chefVm.File.ContentType.Contains("image"))
            {
                ModelState.AddModelError("File", "Duzgun format dakil edin");
                return View();
            }

            if(chefVm.File.Length >= 2097152)
            {
                ModelState.AddModelError("File", "File olcusu boyukdur");
                return View();
            }

            chefVm.ImgUrl = chefVm.File.CreateFile(_env.WebRootPath, "Upload/Chefs");

            Chef chef = new Chef()
            {
                FullName = chefVm.FullName,
                Designation = chefVm.Designation,
                ImgUrl = chefVm.ImgUrl,
                File = chefVm.File,
            };

            await _context.Chefs.AddAsync(chef);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");


        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var chef = await _context.Chefs.FirstOrDefaultAsync(x => x.Id == id);

            if (chef == null)
            {
                return NotFound();
            }

            chef.ImgUrl.RemoveFile(_env.WebRootPath, "Upload/Chefs");

            _context.Chefs.Remove(chef);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }


        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            } 

            var dbChef =_context.Chefs.FirstOrDefault(x => x.Id == id);

            if (dbChef == null)
            { 
                return NotFound(); 
            }

            ChefVm chefVm = new ChefVm()
            {

                FullName = dbChef.FullName,
                Designation = dbChef.Designation,
                ImgUrl = dbChef.ImgUrl,
                File = dbChef.File


            };

            return View(chefVm);

        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, ChefVm chefVm)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Chef dbChef =await _context.Chefs.FirstOrDefaultAsync(x => x.Id == id);

            if(dbChef == null)
            {
                return NotFound(); 
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (chefVm.File != null)
            {
                if (!chefVm.File.ContentType.Contains("image"))
                {
                    ModelState.AddModelError("File", "Duzgun format dakil ele");
                    return View();
                }

                if(chefVm.File.Length >= 2097152)
                {
                    ModelState.AddModelError("File", "File olcusu cox uzundur");
                    return View();
                }

                dbChef.ImgUrl.RemoveFile(_env.WebRootPath, "Upload/Chef");
                dbChef.ImgUrl= dbChef.File.CreateFile(_env.WebRootPath, "Upload/Chef");


            }


            dbChef.FullName = chefVm.FullName;
            dbChef.Designation = chefVm.Designation;    

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");


        }



    }
}

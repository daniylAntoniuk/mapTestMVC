using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Septik.Web.Data;
using Septik.Web.Data.Entities;
using Septik.Web.Models;

namespace Septik.Web.Controllers
{
    public class CitiesController : Controller
    {
        // Create a field to store the mapper object
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        // Assign the object in the constructor for dependency injection
        public CitiesController(IMapper mapper, 
            ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        // GET: CitiesController
        public ActionResult Index()
        {
            var cities = _context.Cities.Select(x=>new CityItemVM() {Name = x.Name, Images = _context.CityImages.Where(t=>t.CityId == x.Id).ToList() }); 
            var dto = _mapper.Map<IEnumerable<CityItemVM>>(cities);
            return View(dto);
        }

        // GET: CitiesController/Details/5
        public ActionResult Details(int id)
        {
            var entity = _context.Cities.SingleOrDefault(c => c.Id == id);
            if(entity != null)
            {
                var model = _mapper.Map<CityDetailsVM>(entity);
                return View(model);
            }
            return NotFound();
        }

        // GET: CitiesController/Create
        public ActionResult Create()
        {
            return View();
        }

        public Image Base64ToImage(string base64String)
        {
            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String.Split(',')[1]);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                Image image = Image.FromStream(ms, true);
                return image;
            }
        }

        // POST: CitiesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CityAddEditVM cityAddEdit, 
            string [] cityImages)
        {
            try
            {
                //string extension;
                //extension = Path.GetExtension(file.FileName);
                ////string f
                //string name = Path.GetRandomFileName() + extension;
                //var path = Path.Combine(
                // Directory.GetCurrentDirectory(), "Uploads", name);
                //using (var stream = new FileStream(path, FileMode.Create))
                //{
                //    await file.CopyToAsync(stream);
                //}

                //var entity = _mapper.Map<City>(cityAddEdit);
                //entity.Image = name;
                //_context.Cities.Add(entity);
                //_context.SaveChanges();

                //return RedirectToAction(nameof(Index));
                //List<CityImage> images = new List<CityImage>();

                var model = new City()
                {
                    Name = cityAddEdit.Name,
                    Lat = cityAddEdit.Lat,
                    Lng = cityAddEdit.Lng,
                    Image = "image"
                };

                _context.Cities.Add(model);
                _context.SaveChanges();
                var entity = _mapper.Map<City>(cityAddEdit);
                foreach (var item in cityImages)
                {
                    Image file = Base64ToImage(item);
                    string extension = ".jpg";
                    string name = Path.GetRandomFileName() + extension;
                    var path = Path.Combine(
                    Directory.GetCurrentDirectory(), "Uploads", name);
                   

                    file.Save(path, ImageFormat.Jpeg);

                    var image = new CityImage();
                    image.Name = name;
                    image.CityId = model.Id;
                    _context.CityImages.Add(image);
                    _context.SaveChanges();
                }
                

                _context.Cities.Add(entity);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));

            }
            catch(Exception ex)
            {
                return View(cityAddEdit);
            }
        }

        // GET: CitiesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CitiesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CitiesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CitiesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

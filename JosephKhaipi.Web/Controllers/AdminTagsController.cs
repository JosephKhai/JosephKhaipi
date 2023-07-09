using JosephKhaipi.Web.Data;
using JosephKhaipi.Web.Models.Domain;
using JosephKhaipi.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace JosephKhaipi.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly JosephKhaiDbContext _context;

        public AdminTagsController(JosephKhaiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
            //Mapping AddTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            _context.Tags.Add(tag);

            _context.SaveChanges();

            return RedirectToAction("List");
        
        }

        [HttpGet]
        [ActionName("List")]
        public IActionResult List()
        {

            // use dbContext to read the Tags
            var tags = _context.Tags.ToList();


            return View(tags);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            //1st method
            //var tag = _context.Tags.Find(id);

            //2nd method
            var tag = _context.Tags.FirstOrDefault(x => x.Id == id);

            if(tag != null)
            {
                var ediTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(ediTagRequest);
            }

            return View(null);
        }

        [HttpPost]
        public IActionResult Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };

            var existingTag =  _context.Tags.Find(tag.Id);

            if(existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                _context.SaveChanges();

                //show success notification
                return RedirectToAction("Edit", new { id = editTagRequest.Id });
                //return RedirectToAction("List");
            }
           
            //show error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });

            
        }

        [HttpPost]
        public IActionResult Delete(EditTagRequest editTagRequest)
        {
            var existingTag = _context.Tags.Find(editTagRequest.Id);

            if(existingTag != null)
            {
                _context.Tags.Remove(existingTag);
                _context.SaveChanges();

                //show a success notification
                return RedirectToAction("List");
            }

            //show an error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id});
        }
    }
}

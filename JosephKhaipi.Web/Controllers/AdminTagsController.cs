using JosephKhaipi.Web.Data;
using JosephKhaipi.Web.Models.Domain;
using JosephKhaipi.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            //Mapping AddTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            await _context.Tags.AddAsync(tag);

            await _context.SaveChangesAsync();

            return RedirectToAction("List");
        
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {

            // use dbContext to read the Tags
            var tags = await _context.Tags.ToListAsync();

            
            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //1st method
            //var tag = _context.Tags.Find(id);

            //2nd method
            var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == id);

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
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };

            var existingTag = await _context.Tags.FindAsync(tag.Id);

            if(existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await _context.SaveChangesAsync();

                //show success notification
                return RedirectToAction("Edit", new { id = editTagRequest.Id });
                //return RedirectToAction("List");
            }
           
            //show error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });

            
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var existingTag = await _context.Tags.FindAsync(editTagRequest.Id);

            if(existingTag != null)
            {
                _context.Tags.Remove(existingTag);
                await _context.SaveChangesAsync();

                //show a success notification
                return RedirectToAction("List");
            }

            //show an error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id});
        }
    }
}

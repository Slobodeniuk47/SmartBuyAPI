using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Drawing;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using SmartBuyAPI.Data;
using SmartBuyAPI.Models.DTO;
using SmartBuyAPI.Models.DTO.Category;
using Microsoft.Extensions.Configuration;
using SmartBuyAPI.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartBuyAPI.Helpers;
using SmartBuyAPI.Models.DTO.Product;

namespace SmartBuyAPI.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _iconfiguration;

        public CategoryController(ApplicationDbContext context, IConfiguration iconfigoration, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _iconfiguration = iconfigoration;
        }

        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<CategoryItemDTO>>> Get()
        {
            var result = await _context.Categories.Select(x => _mapper.Map<CategoryItemDTO>(x)).ToListAsync();
            return Ok(result);
        }
        [HttpGet("get/{id}")] //[HttpGet("get/{id:int}")]
        public async Task<ActionResult<IEnumerable<CategoryItemDTO>>> GetById(int id)
        {
            var result = await _context.Categories.Where(x => x.Id == id).Select(x => _mapper.Map<CategoryItemDTO>(x)).ToListAsync();
            if (result.Count > 0)
            {
                return new ObjectResult(result[0]);
            }
            else { return NotFound(); }
        }
        /// <summary>
        /// Создание категории
        /// </summary>
        /// <param name="model">Категория</param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ActionResult<IEnumerable<CategoryCreateDTO>>> Create([FromBody] CategoryCreateDTO model)
        {
            var category = _mapper.Map<CategoryEntity>(model); // CategoryEntity update CategoryCreateDTO

            category.DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
            category.Image = ImageHelper.SaveAndGetImageName(model.Image, _iconfiguration);

            await _context.AddAsync(category);
            await _context.SaveChangesAsync();
            return Ok(category);
        }
        
        [HttpPut("edit")]
        public async Task<ActionResult<IEnumerable<CategoryEditDTO>>> Edit([FromForm] CategoryEditDTO model)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (category == null)
                return NotFound();

            ImageHelper.DeleteImage(category.Image);

            category.Name = model.Name;
            category.DisplayOrder = model.DisplayOrder;
            category.DateLastEdit = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

            string imageNewName = ImageHelper.SaveAndGetImageName(model.ImageUpload, _iconfiguration);
            category.Image = string.IsNullOrEmpty(imageNewName) ? category.Image : imageNewName;

            category.Priority = model.Priority;
            category.Description = model.Description;

            await _context.SaveChangesAsync();
            return Ok(category);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound();

            //var imageOld = category.Image;
            ImageHelper.DeleteImage(category.Image);
            
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}

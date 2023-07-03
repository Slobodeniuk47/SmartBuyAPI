using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Drawing;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using SmartBuyAPI.Data;
using SmartBuyAPI.Models.DTO;
using Microsoft.Extensions.Configuration;
using SmartBuyAPI.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartBuyAPI.Helpers;
using SmartBuyAPI.Models.DTO.Product;
using SmartBuyAPI.Models.DTO.Category;

namespace SmartBuyAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _iconfiguration;

        public ProductController(ApplicationDbContext context, IConfiguration iconfigoration, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _iconfiguration = iconfigoration;
        }

        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<ProductItemDTO>>> Get()
        {
            var result = await _context.Products.Select(x => _mapper.Map<ProductItemDTO>(x)).ToListAsync();
            return Ok(result);
        }
        [HttpGet("get/{id:int}")]
        public async Task<ActionResult<IEnumerable<ProductItemDTO>>> GetById(int id)
        {
            var result = await _context.Products.Where(x => x.Id == id).Select(x => _mapper.Map<ProductItemDTO>(x)).ToListAsync();
            if (result.Count > 0)
            {
                return new ObjectResult(result[0]);
            }
            else { return NotFound(); }
        }
        [HttpPost("create")]
        public async Task<ActionResult<IEnumerable<ProductCreateDTO>>> Create([FromForm] ProductCreateDTO model)
        {
            
            var product = _mapper.Map<ProductEntity>(model); // CategoryEntity update CategoryCreateDTO
            product.DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
            product.Image = ImageHelper.SaveAndGetImageName(model.Image, _iconfiguration);
            
            product.CategoryId = model.CategoryId == 0 ? null : model.CategoryId;
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpPut("edit")]
        public async Task<ActionResult<IEnumerable<ProductEditDTO>>> Edit([FromForm] ProductEditDTO model)
        {
            var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == model.Id);          
            if (product == null) 
                return NotFound();

            product.Name = model.Name;
            product.DateLastEdit = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
            
            ImageHelper.DeleteImage(product.Image);
            string imageNewName = ImageHelper.SaveAndGetImageName(model.ImageUpload, _iconfiguration);
            product.Image = string.IsNullOrEmpty(imageNewName) ? product.Image : imageNewName;

            product.Description = model.Description;
            product.ShortDescription = model.ShortDescription;
            product.Price = model.Price;
            product.CategoryId = model.CategoryId == 0 ? null : model.CategoryId;
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == id);
            if (product == null)
                return NotFound();

            ImageHelper.DeleteImage(product.Image);

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test2.Models;

namespace Test2.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HotelsController : ControllerBase
	{
		HotelsContext db;

		public HotelsController(HotelsContext context)
		{
			db = context;

			if (!db.Hotels.Any())
			{
				db.Hotels.Add(new Hotel { Name = "Mayami", Address = "Красногеройская улица, 20" });
				db.Hotels.Add(new Hotel { Name = "Витязь", Address = "Советская улица, 30" });
				db.SaveChanges();
			}
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Hotel>>> Get()
		{
			return await db.Hotels.ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Hotel>> Get(int id)
		{
			var hotel = db.Hotels.FirstOrDefaultAsync(x => x.Id == id);

			if (hotel == null)
			{
				return NotFound();
			}

			return new ObjectResult(hotel);
		}

		[HttpPost]
		public async Task<ActionResult<Hotel>> Post(Hotel hotel)
		{
			if (hotel == null)
			{
				return BadRequest();
			}

			db.Hotels.Add(hotel);
			await db.SaveChangesAsync();

			return Ok(hotel);
		}

		[HttpPut]
		public async Task<ActionResult<Hotel>> Put(Hotel hotel)
		{
			if (hotel == null)
			{
				return BadRequest();
			}

			if (!db.Hotels.Any(x => x.Id == hotel.Id))
			{
				return NotFound();
			}

			db.Update(hotel);
			await db.SaveChangesAsync();

			return Ok(hotel);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<Hotel>> Delete(int id)
		{
			var hotel = db.Hotels.FirstOrDefault(x => x.Id == id);

			if (hotel == null)
			{
				return NotFound();
			}

			db.Hotels.Remove(hotel);
			await db.SaveChangesAsync();

			return Ok(hotel);
		}
	}
}

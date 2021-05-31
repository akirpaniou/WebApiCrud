using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIMet.Models;

namespace WebAPIMet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        CoursesContext db;
        public CoursesController(CoursesContext context)
        {
            db = context;
            if (!db.Courses.Any())
            {
                db.Courses.Add(new Course { Name = "Csharp", Opis = "Course Csharp" });
                db.Courses.Add(new Course { Name = "Python", Opis = "Course python" });
                db.SaveChanges();
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> Get()
        {
            return await db.Courses.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> Get(int id)
        {
            Course course = await db.Courses.FirstOrDefaultAsync(x => x.id == id);
            if (course == null)
                return NotFound();
            return new ObjectResult(course);
        }
        
        
        [HttpPost]
        public async Task<ActionResult<Course>> Post(Course course)
        {
            if (course == null)
            {
                return BadRequest();
            }
            db.Courses.Add(course);
            await db.SaveChangesAsync();
            return Ok(course);
        }

        
        [HttpPut]
        public async Task<ActionResult<Course>> Put(Course course)
        {
            if (course == null)
            {
                return BadRequest();
            }
            if (!db.Courses.Any(x => x.id == course.id))
            {
                return NotFound();
            }
            db.Update(course);
            await db.SaveChangesAsync();
            return Ok(course);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Course>> Delete(int id)
        {
            Course course = db.Courses.FirstOrDefault(x => x.id == id);
            if (course == null)
            {
                return NotFound();
            }
            db.Courses.Remove(course);
            await db.SaveChangesAsync();
            return Ok(course);
        }
    } 
}

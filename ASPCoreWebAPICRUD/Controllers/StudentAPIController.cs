using ASPCoreWebAPICRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPCoreWebAPICRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly MyDbContext context;
        public StudentAPIController(MyDbContext context) 
        {
        this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {

            var data = await context.Students.ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var student = await context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return student;
        }
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student obj)
        {
            await context.Students.AddAsync(obj);
            await context.SaveChangesAsync();
            return Ok(obj);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateStudent(int id,Student obj)
        {
            if(id!= obj.Id)
            {
                return BadRequest();
            }
            context.Entry(obj).State= EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(obj);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var obj = await context.Students.FindAsync(id);
            if(obj == null)
            {
                return NotFound();
            }
            context.Students.Remove(obj);
            await context.SaveChangesAsync();
            return Ok();

        }
    }
}

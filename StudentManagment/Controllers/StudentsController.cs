using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StudentManagment.Models;
using StudentManagment.Interfaces;

namespace StudentManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentsController(IStudentService service)
        {
            _service = service;
        }

        // GET: api/Students
        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            var items = _service.GetAllItems();
            return Ok(items);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            var student =  _service.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // POST: api/Students
        [HttpPost]
        public ActionResult PostStudent([FromBody]Student student)
        {
             if(!ModelState.IsValid)
             {
                return BadRequest(ModelState);
             }

            var item = _service.Add(student);
            return CreatedAtAction("GetStudent", new { id = item.Id }, item);
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public IActionResult PutStudent(int Id, [FromBody]Student student)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingItem = _service.GetById(Id);

            if (existingItem == null)
            {
                return NotFound();
            }

            var item = _service.Update(Id, student);
            return CreatedAtAction("GetStudent", new { id = Id }, item);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public ActionResult DeleteStudent(int id)
        {
            var existingItem = _service.GetById(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            _service.Remove(id);
            return Ok();
        }

    }
}

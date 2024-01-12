using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace StudentApiController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController
    {
        private static List<Student> _students = new List<Student>()
        {
            new Student { Id = 1, FirstName = "Paul", LastName = "Ochon", Birthdate = new DateTime(1950, 12, 1) },
            new Student { Id = 2, FirstName = "Daisy", LastName = "Drathey", Birthdate = new DateTime(1970, 12, 1) },
            new Student { Id = 3, FirstName = "Elie", LastName = "Coptaire", Birthdate = new DateTime(1980, 12, 1) }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            return _students;
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetStudentByID(int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            return student;
        }

        [HttpPost]
        public ActionResult<Student> AddStudent([FromBody] Student student)
        {
            if (student == null)
            {
                return null;
            }

            student.Id = _students.Count + 1;
            _students.Add(student);
            return student;
        }
    }
}
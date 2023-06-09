using Exercise3.Models;
using Exercise3.Models.DTOs;
using Exercise3.Repositories;
using Exercise3.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Exercise3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsRepository _studentsRepository;
        public StudentsController(IStudentsRepository studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            
            var students = _studentsRepository.GetStudents();
            return Ok(students);
        }

        [HttpGet("{index}")]
        public async Task<IActionResult> Get(string index)
        {
            var students = (List<Student>)_studentsRepository.GetStudents();
            var student = students.ToList().Find(e => e.IndexNumber == index);

            if(student == null) return NotFound();
            return Ok(student); 
        }

        [HttpPut("{index}")]    
        public async Task<IActionResult> Put(string index, StudentPUT newStudentData)
        {
            var student = _studentsRepository.GetStudents().FirstOrDefault(s => s.IndexNumber == index);
            if(student == null) return NotFound();

            var newData = new Student
            {
                FirstName = newStudentData.FirstName ?? student.FirstName,
                LastName = newStudentData.LastName ?? student.LastName,
                IndexNumber = index,
                BirthDate = newStudentData.BirthDate ?? student.BirthDate,
                StudyName = newStudentData.StudyName ?? student.StudyName,
                StudyMode = newStudentData.StudyMode ?? student.StudyMode,
                Email = newStudentData.Email ?? student.Email,
                FathersName = newStudentData.FathersName ?? student.FathersName,
                MothersName = newStudentData.MothersName ?? student.MothersName
            };
            return Ok(newData);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(StudentPOST newStudent)
        {
            try
            {
                var indexExists = _studentsRepository.GetStudents().FirstOrDefault(s => s.IndexNumber == newStudent.IndexNumber);
                if (indexExists != null)
                {
                    return Conflict("Student with this index number already exists.");
                }

                if (string.IsNullOrEmpty(newStudent.FirstName) ||
                    string.IsNullOrEmpty(newStudent.LastName) ||
                    string.IsNullOrEmpty(newStudent.IndexNumber) ||
                    string.IsNullOrEmpty(newStudent.BirthDate) ||
                    string.IsNullOrEmpty(newStudent.StudyName) ||
                    string.IsNullOrEmpty(newStudent.StudyMode) ||
                    string.IsNullOrEmpty(newStudent.Email) ||
                    string.IsNullOrEmpty(newStudent.FathersName) ||
                    string.IsNullOrEmpty(newStudent.MothersName))
                {
                    return BadRequest("All fields are required.");
                }

                var student = new Student
                {
                    FirstName = newStudent.FirstName,
                    LastName = newStudent.LastName,
                    IndexNumber = newStudent.IndexNumber,
                    BirthDate = newStudent.BirthDate,
                    StudyName = newStudent.StudyName,
                    StudyMode = newStudent.StudyMode,
                    Email = newStudent.Email,
                    FathersName = newStudent.FathersName,
                    MothersName = newStudent.MothersName
                };


                return Created("", null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpDelete("{index}")]
        public async Task<IActionResult> Delete(string index)
        {
                var students = _studentsRepository.GetStudents().ToList();
                var studentToDelete = students.FirstOrDefault(s => s.IndexNumber == index);

            if (studentToDelete == null) return NotFound();
            return Ok(studentToDelete);

        }

    }
}

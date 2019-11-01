using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentManagment.Interfaces;
using StudentManagment.Models;

namespace StudentManagment.Services
{
    public class StudentService : IStudentService
    {
        private readonly StudentManagmentContext _context;

        public StudentService(StudentManagmentContext context) {
            _context = context;
        }

        public Student Add(Student student)
        {
             _context.Student.Add(student);
            _context.SaveChanges();
            return student;
        }

        public IEnumerable<Student> GetAllItems()
        {
            return _context.Student.ToList();
        }

        public Student GetById(int id)
        {
            return _context.Student.FirstOrDefault(student => student.Id == id);
        }

        public Student Update(int id, Student student)
        {
            var entity = _context.Student.FirstOrDefault(e => e.Id == id);

            if (entity != null)
            {
                entity.Name = student.Name;
                _context.SaveChanges();
                return entity;
            }
            return null;
        }

        public void Remove(int id)
        {
            var student = _context.Student.FirstOrDefault(student => student.Id == id);
            if (student != null)
            {
                _context.Student.Remove(student);
                _context.SaveChanges();
            }
        }

    }
}

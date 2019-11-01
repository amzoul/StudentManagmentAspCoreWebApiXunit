using System;
using System.Linq;
using System.Threading.Tasks;
using StudentManagment.Interfaces;
using StudentManagment.Models;
using System.Collections.Generic;
namespace StudentManagment.Services
{
    public class StudentServiceFake : IStudentService
    {
        private readonly System.Collections.Generic.List<Student> _students;


        public StudentServiceFake()
        {
            _students = new List<Student>()
            {
                new Student (){Id = 1001, Name="AMZA MOHAMED"},
                new Student (){Id = 1002, Name="ALAIN BELO EKOLO"},
                new Student (){Id = 1003, Name="ELENE EDA"}
            };
        }
        

        public IEnumerable<Student> GetAllItems()
        {
            return _students;
        }

        public Student GetById(int id)
        {
            return _students.Where(student => student.Id == id).FirstOrDefault();
        }
        public Student Add(Student student)
        {
            //create a integer uniqid
            var now = DateTime.Now;
            var zeroDate = DateTime.MinValue.AddHours(now.Hour).AddMinutes(now.Minute).AddSeconds(now.Second).AddMilliseconds(now.Millisecond);
            int uniqueId = (int)(zeroDate.Ticks / 10000);

            student.Id = uniqueId;
            _students.Add(student);
            return student;
        }
        public Student Update(int id, Student student)
        {
            if (student == null)
            {
                return null;
            }
            int index = _students.FindIndex(student => student.Id == id);
            if (index == -1)
            {
                return null;
            }
            _students.RemoveAt(index);
            _students.Add(student);
            return student;
        }

        public void Remove(int id)
        {
            var existing = _students.FirstOrDefault(student => student.Id == id);
            _students.Remove(existing);
        }

      
    }
}

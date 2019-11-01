using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentManagment.Models;

namespace StudentManagment.Interfaces
{
   public  interface IStudentService
    {
        IEnumerable<Student> GetAllItems();
        Student GetById(int id);
        Student Add(Student student);
        Student Update(int id, Student student);
        void Remove(int id);

    }
}

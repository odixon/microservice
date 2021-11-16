using System;
using DapperExtensions.Mapper;

namespace WebApplication.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
    }

    public class StudentMapper : ClassMapper<Student>
    {
        public StudentMapper()
        {
            Table("Students");
            AutoMap();
        }
    }
}

using CourseApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CourseApplication.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult AppList()
        {
            List<Student> students = GetStudentsFromDB();

            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.Id}");
                Console.WriteLine($"First Name: {student.FirstName}");
                Console.WriteLine($"Last Name: {student.LastName}");
                Console.WriteLine($"Age: {student.Age}");
                Console.WriteLine($"Email: {student.Email}");
                Console.WriteLine("-----------------------");
            }

            return View("~/Views/Home/AppList.cshtml", students);
        }

        private readonly IConfiguration _configuration;

        public StudentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        private List<Student> GetStudentsFromDB()
        {
            List<Student> students = new List<Student>();

            //string connectionString = _configuration.GetConnectionString("UniversityConnection");
            string connectionString = "server=95.164.11.251;database=University;user=kourseWork ;password=Inanih71239856*;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Students";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Student student = new Student
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Age = reader.GetInt32(3),
                        Email = reader.GetString(4)
                    };

                    students.Add(student);
                }

                reader.Close();
            }

            return students;
        }


    }
}

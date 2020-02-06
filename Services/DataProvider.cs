using Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Services
{
    public class DataProvider
    {
        private static string connection = ConfigurationManager.ConnectionStrings["PrivateSchool"].ConnectionString;
        private static readonly List<string> queries = new List<string>()
        {
            "Select * from Student", //GetAllStudents
            "Select * from trainer t inner join Subject s on s.TrainerID = t.id;", //GetAllTrainers
            "Select * from Assignment a inner join Grade g on g.AssignmentID = a.ID", //GetAllAssignments
            "Select * from Course", //GetAllCourses
            "Select sc.ID,c.Title, s.FirstName,s.LastName from Course c inner join StudentCourse sc on sc.CourseID = c.ID inner join Student s on s.ID = sc.StudentID", //GetStudentPerCourses
            "Select c.Title,t.FirstName, t.LastName from Course c inner join Trainer t on t.ID = c.TrainerID;", //GetTrainerPerCourses
            "Select c.ID, c.Title,a.Title from Course c inner join Assignment a on a.ID = c.AssignmentID order by c.Id;", //GetAssignmentPerCourses
            "Select a.Title,c.Title, s.FirstName, s.LastName from Course c inner join StudentCourse sc on sc.CourseID = c.ID" + //GetAssigmentPerCoursesPerStudent
                    " inner join Student s on s.ID = sc.StudentID inner join Assignment a on a.ID = c.AssignmentID;",
            "Select s.FirstName , s.LastName,Count(c.ID) as totalCourse from Course c inner join StudentCourse sc on sc.CourseID = c.ID " + //GetStudentCourseCount
            "inner join Student s on s.ID = sc.StudentID group by s.FirstName , s.LastName having COUNT(c.ID) > 1;"
        };
        //Gets All students returning a list<Student>
        public static List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(queries[0], con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var id = Convert.ToInt32(reader[0]);
                    var firstName = reader[1].ToString();
                    var lastName = reader[2].ToString();
                    decimal? tuitionFees = string.IsNullOrWhiteSpace(reader[3].ToString()) ? (decimal?)null : Convert.ToDecimal(reader[3]);
                    DateTime? dateOfBirth = string.IsNullOrWhiteSpace(reader[4].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader[4]);
                    students.Add(new Student(id, firstName, lastName, tuitionFees, dateOfBirth));
                }
            }
            return students;
        }
        //Gets All Trainers returning a list<Trainer>
        public static List<Trainer> GetAllTrainers()
        {
            List<Trainer> trainers = new List<Trainer>();
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(queries[1], con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var id = Convert.ToInt32(reader[0]);
                    var firstName = reader[1].ToString();
                    var lastName = reader[2].ToString();
                    var subject = reader[4].ToString();
                    trainers.Add(new Trainer(id, firstName, lastName, subject));
                }
            }
            return trainers;
        }
        //Gets All ASsignments returning a list<Assignment>
        public static List<Assignment> GetAllAssignments()
        {
            List<Assignment> assignments = new List<Assignment>();
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(queries[2], con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var id = Convert.ToInt32(reader[0]);
                    var description = string.IsNullOrWhiteSpace(reader[1].ToString()) ? null : reader[1].ToString();
                    var title = reader[2].ToString();
                    DateTime? subDate = string.IsNullOrWhiteSpace(reader[3].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader[3]);
                    var oralMark = Convert.ToSingle(reader[5]);
                    var totalmark = Convert.ToSingle(reader[6]);

                    assignments.Add(new Assignment(id, description, title, subDate, oralMark, totalmark));
                }
            }
            return assignments;
        }
        //Gets All courses returning a list<Course>
        public static List<Course> GetAllCourses()
        {
            List<Course> courses = new List<Course>();
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(queries[3], con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var id = Convert.ToInt32(reader[0]);
                    var title = reader[1].ToString();
                    var stream = reader[2].ToString();
                    var type = reader[3].ToString();
                    DateTime? startDate = string.IsNullOrWhiteSpace(reader[6].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader[6]);
                    DateTime? endDate = string.IsNullOrWhiteSpace(reader[7].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader[7]);
                    courses.Add(new Course(id, title, stream, type, startDate, endDate));
                }
            }
            return courses;
        }
        //Gets Students related to course returning a list<StudentCourse>
        public static List<StudentCourse> GetStudentPerCourses()
        {
            List<StudentCourse> studentPerCourses = new List<StudentCourse>();
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(queries[4], con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var id = Convert.ToInt32(reader[0]);
                    var title = reader[1].ToString();
                    var firstName = reader[2].ToString();
                    var lastName = reader[3].ToString();

                    studentPerCourses.Add(new StudentCourse(id, title, firstName, lastName));
                }
            }
            return studentPerCourses;
        }
        //Gets Trainers related to course returning a list<TrainerCourse>
        public static List<TrainerCourse> GetTrainerPerCourses()
        {
            List<TrainerCourse> trainerCourses = new List<TrainerCourse>();
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(queries[5], con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var courseTitle = reader[0].ToString();
                    var firstName = reader[1].ToString();
                    var lastName = reader[2].ToString();

                    trainerCourses.Add(new TrainerCourse(courseTitle, firstName, lastName));
                }
            }
            return trainerCourses;
        }
        //Gets Assignments related to course returning a list<AssignmentCourse>
        public static List<AssignmentCourse> GetAssignmentPerCourses()
        {
            List<AssignmentCourse> assignmentPerCourses = new List<AssignmentCourse>();
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(queries[6], con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var id = Convert.ToInt32(reader[0]);
                    var courseTitle = reader[1].ToString();
                    var assignmentTitle = reader[2].ToString();

                    assignmentPerCourses.Add(new AssignmentCourse(id, courseTitle, assignmentTitle));
                }
            }
            return assignmentPerCourses;

        }
        //Gets Assignments related to a course and Student returning a list<AssignmentCourseStudent>
        public static List<AssignmentCourseStudent> GetAssigmentPerCoursesPerStudent()
        {
            List<AssignmentCourseStudent> assigmentCourseStudents = new List<AssignmentCourseStudent>();
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(queries[7], con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var assignmentTitle = reader[0].ToString();
                    var courseTitle = reader[1].ToString();
                    var studentFirstName = reader[2].ToString();
                    var studentLastName = reader[3].ToString();

                    assigmentCourseStudents.Add(new AssignmentCourseStudent(assignmentTitle, courseTitle, studentFirstName, studentLastName));
                }
            }
            return assigmentCourseStudents;
        }
        //Gets Student that has more than one course also returning a list<StudentCourseCount>
        public static List<StudentCourseCount> GetStudentCourseCount()
        {
            List<StudentCourseCount> studentCourseCount = new List<StudentCourseCount>();
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(queries[8], con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var studentFirstName = reader[0].ToString();
                    var studentLastName = reader[1].ToString();
                    var countOfCourses = Convert.ToInt32(reader[2]);
                    studentCourseCount.Add(new StudentCourseCount(studentFirstName, studentLastName, countOfCourses));
                }
            }
            return studentCourseCount;
        }
    }
}

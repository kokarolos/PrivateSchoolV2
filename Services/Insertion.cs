using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Services
{
    public class Insertion
    {
        private static readonly string connection = ConfigurationManager.ConnectionStrings["PrivateSchool"].ConnectionString;
        //Method responsible to Insert a student into db 
        public static void InsertStudent(string firstName, string lastName, decimal? tuitionFees, DateTime? dateOfBirth)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                var query = "Insert Into Student(FirstName, LastName, TuitionFees, DateOfBirth) values(@FirstName, @LastName, @TuitionFees, @DateOfBirth)";
                SqlCommand cmd = new SqlCommand(query, con);
                //Passing Values To Parameters
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@TuitionFees", tuitionFees ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth ?? (object)DBNull.Value);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Student Inserted Successfully");
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }
        //Method responsible to Insert a Trainer into db 
        public static void InsertTrainer(string firstName, string lastName, string subject)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                var Trainerquery = "Insert Into Trainer(FirstName, LastName) values(@FirstName, @LastName) SELECT SCOPE_IDENTITY()";
                var subjectQuery = "Insert into Subject(Title,TrainerID) values(@Title, @TrainerID);";
                SqlCommand cmd = new SqlCommand(Trainerquery, con);

                //Passing Values To Parameters
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    var insertedId = cmd.ExecuteScalar();
                    SqlCommand subcmd = new SqlCommand(subjectQuery, con);
                    subcmd.Parameters.AddWithValue("@TrainerID", Convert.ToInt32(insertedId));
                    subcmd.Parameters.AddWithValue("@Title", subject);
                    subcmd.ExecuteNonQuery();
                    Console.WriteLine("Trainer Inserted Successfully");
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }
        //Method responsible to Insert an Assignment into db 
        public static void InsertAssignment(string description, string title, DateTime? subDate, float oralMark, float totalMark)
        {
            var gradeQuery = "Insert into Grade(OralMark,TotalMark,AssignmentID) Values(@OralMark,@TotalMark,@AssignmentID)";
            var assignmentQuery = "Insert Into Assignment(Description, Title, SubDate) values(@Description, @Title, @SubDate) SELECT SCOPE_IDENTITY()";

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand(assignmentQuery, con);

                //Passing Values To Parameters
                cmd.Parameters.AddWithValue("@Description", string.IsNullOrWhiteSpace(description) ? description : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@SubDate", subDate ?? (object)DBNull.Value);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    var insertedId = cmd.ExecuteScalar();
                    SqlCommand gradecmd = new SqlCommand(gradeQuery, con);
                    gradecmd.Parameters.AddWithValue("@OralMark", oralMark);
                    gradecmd.Parameters.AddWithValue("@TotalMark", totalMark);
                    gradecmd.Parameters.AddWithValue("@AssignmentID", insertedId);
                    gradecmd.ExecuteNonQuery();
                    Console.WriteLine("Assignment Inserted Successfully");
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }
        //Method responsible to Insert a Course into db 
        public static void InsertCourse(string title, string stream, string type, DateTime? startDate, DateTime? endDate)
        {
            var courseQuery = "Insert into Course(Title,Stream,Type,StartDate,EndDate) values(@Title,@Stream,@Type,@StartDate,@EndDate)";

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand(courseQuery, con);

                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@Stream", stream);
                cmd.Parameters.AddWithValue("@Type", type);
                cmd.Parameters.AddWithValue("@StartDate", startDate ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@EndDate", endDate ?? (object)DBNull.Value);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Course Inserted Successfully");
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                }
                finally
                {
                    con.Close();
                }

            }
        }
        //Method responsible to Insert a StudentPerCourse into db 
        public static void InsertStudentsPerCourse(int StudentID, int CourseID)
        {
            var studentPerCourseQuery = "Insert into StudentCourse(StudentID,CourseID) values(@StudentID,@CourseID)";

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand(studentPerCourseQuery, con);

                cmd.Parameters.AddWithValue("@StudentID", StudentID);
                cmd.Parameters.AddWithValue("@CourseID", CourseID);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Student Inserted Succesfully into course");
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }
        //Method responsible to Insert a TrainerPerCourse into db 
        public static void InsertTrainersPerCourse(int courseID, int trainerID)
        {
            var coursequery =
                "Update Course Set TrainerID = TrainerID where Course.ID = @CourseID;";

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand(coursequery, con);

                cmd.Parameters.AddWithValue("@TrainerID", trainerID);
                cmd.Parameters.AddWithValue("@CourseID", courseID);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Trainer Inserted Succesfully into Course");
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }
        //Method responsible to Insert an AssignementPerStudentPerCourse into db 
        public static void InsertAssignmentPerStudentPerCourse(int asssignmentId, int courseId, int studentID)
        {
            var CourseAssignmentQuery = " Update Course Set AssignmentID = @AssignmentId where Course.ID = @CID";
            var StudentCourseQuery = "Insert into StudentCourse(StudentID,CourseID) Values(@StudentID,@CourseID)";

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand(CourseAssignmentQuery, con);

                //Passing Values To Parameters
                cmd.Parameters.AddWithValue("@AssignmentId", asssignmentId);
                cmd.Parameters.AddWithValue("@CID", courseId);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlCommand scmd = new SqlCommand(StudentCourseQuery, con);
                    scmd.Parameters.AddWithValue("@StudentID", studentID);
                    scmd.Parameters.AddWithValue("@CourseID", courseId);
                    scmd.ExecuteNonQuery();
                    Console.WriteLine("Assignmnet Per Course Per Student Inserted Successfully");
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                }
                finally
                {
                    con.Close();
                }
            }




        }


    }
}

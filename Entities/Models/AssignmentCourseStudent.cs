using System;

namespace Entities
{
    public class AssignmentCourseStudent : IPrintable
    {
        public string AssignmentTitle { get; set; }
        public string CourseTitle { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public AssignmentCourseStudent(string assignmentTitle, string courseTitle, string studentFirstName, string studentLastName)
        {
            AssignmentTitle = assignmentTitle;
            CourseTitle = courseTitle;
            StudentFirstName = studentFirstName;
            StudentLastName = studentLastName;
        }
        public void Print()
        {
            Console.WriteLine("{0,5}\t{1,20}\t{2,35}{3,45}", AssignmentTitle, CourseTitle, StudentFirstName, StudentLastName);
        }
    }
}

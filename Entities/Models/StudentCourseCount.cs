using System;

namespace Entities
{
    public class StudentCourseCount : IPrintable
    {
        //class that provide students that belong to more than one courses
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public int CourseCount { get; set; }
        public StudentCourseCount(string studentFirstName, string studentLastName, int courseCount)
        {
            StudentFirstName = studentFirstName;
            StudentLastName = studentLastName;
            CourseCount = courseCount;
        }

        public void Print()
        {
            Console.WriteLine("{0,15}{1,15}{2,5}", StudentFirstName, StudentLastName, CourseCount);
        }
    }
}

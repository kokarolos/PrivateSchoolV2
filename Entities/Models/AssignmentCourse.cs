using System;

namespace Entities
{
    public class AssignmentCourse : IPrintable
    {

        public int Id { get; set; }
        public string courseTitle { get; set; }
        public string AssignmentTitle { get; set; }

        public AssignmentCourse(int id, string courseTitle, string assignmentTitle)
        {
            Id = id;
            this.courseTitle = courseTitle;
            AssignmentTitle = assignmentTitle;
        }

        public void Print()
        {
            Console.WriteLine("{0,2}\t{1,5}{2,35}", Id, courseTitle, AssignmentTitle);
        }
    }
}

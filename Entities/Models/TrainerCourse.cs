using System;

namespace Entities
{
    public class TrainerCourse : IPrintable
    {
        public string CourseTitle { get; set; }
        public string TrainerFirstName { get; set; }
        public string TrainerLastName { get; set; }

        public TrainerCourse(string courseTitle, string trainerFirstName, string trainerLastName)
        {
            CourseTitle = courseTitle;
            TrainerFirstName = trainerFirstName;
            TrainerLastName = trainerLastName;
        }
        public void Print()
        {
            Console.WriteLine("{0,5}{1,10}{2,15}", CourseTitle, TrainerFirstName, TrainerLastName);
        }
    }
}

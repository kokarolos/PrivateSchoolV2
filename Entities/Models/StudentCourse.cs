using System;

namespace Entities
{
    public class StudentCourse : IPrintable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public StudentCourse(int id, string title, string firstName, string lastName)
        {
            Id = id;
            Title = title;
            FirstName = firstName;
            LastName = lastName;
        }

        public void Print()
        {
            Console.WriteLine("{0,-5}{1,5}{2,15}{3,15}", Id, Title, FirstName, LastName);
        }
    }
}

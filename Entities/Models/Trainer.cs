using System;

namespace Entities
{
    public class Trainer : IPrintable
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Subject { get; set; }
        public Trainer(int iD, string firstName, string lastName, string subject)
        {
            ID = iD;
            FirstName = firstName;
            LastName = lastName;
            Subject = subject;
        }
        public void Print()
        {
            Console.WriteLine("{0,2}{1,15}{2,25}{3,45}", ID, FirstName, LastName, Subject);
        }
    }
}

using System;

namespace Entities
{
    public class Student : IPrintable
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal? TuitionFees { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Student(int iD, string firstName, string lastName, decimal? tuitionFees, DateTime? dateOfBirth)
        {
            ID = iD;
            FirstName = firstName;
            LastName = lastName;
            TuitionFees = tuitionFees;
            DateOfBirth = dateOfBirth;
        }
        public void Print()
        {
            Console.WriteLine("{0,2}{1,10}{2,15}{3,15}{4,20}", ID, FirstName, LastName, TuitionFees, DateOfBirth.HasValue ? DateOfBirth.Value.ToString("dd-MM-yyyy") : (object)DBNull.Value);
        }
    }
}

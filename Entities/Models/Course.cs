using System;

namespace Entities
{
    public class Course : IPrintable
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Stream { get; set; }
        public string Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Course(int iD, string title, string stream, string type, DateTime? startDate, DateTime? endDate)
        {
            ID = iD;
            Title = title;
            Stream = stream;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
        }
        public void Print()
        {

            var outputStartDate = StartDate.HasValue ? StartDate.Value.ToString("dd-MM-yyyy") : (object)DBNull.Value; //assign all this condition to variable so it is readable
            var outputEndDate = EndDate.HasValue ? EndDate.Value.ToString("dd-MM-yyyy") : (object)DBNull.Value;
            Console.WriteLine("{0,2}{1,12}{2,12}{3,12}{4,12}{5,12}", ID, Title, Stream, Type, outputStartDate, outputEndDate);
        }

    }
}

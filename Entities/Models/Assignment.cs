using System;

namespace Entities
{
    public class Assignment : IPrintable
    {
        public int ID { get; set; }
        public string Description { get; set; } //nullable but string
        public string Title { get; set; }
        public DateTime? SubDate { get; set; } //nullable 
        public float OralMark { get; set; }
        public float Totalmark { get; set; }

        public Assignment(int iD, string description, string title, DateTime? subDate, float oralMark, float totalmark)
        {
            ID = iD;
            Description = description;
            Title = title;
            SubDate = subDate;
            OralMark = oralMark;
            Totalmark = totalmark;
        }

        public void Print()
        {
            var outputsubDate = SubDate.HasValue ? SubDate.Value.ToString("dd-MM-yyyy") : (object)DBNull.Value;
            Console.WriteLine("{0,2}\t{1,25}\t{2,35}\t{3,45}{4,55}{5,15}", ID, Description, Title, outputsubDate, OralMark, Totalmark);
        }
    }
}

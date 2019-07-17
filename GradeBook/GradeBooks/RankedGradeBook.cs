using System;
using GradeBook.Enums;
using System.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked grading requires at least 5 students.");
            }

            //Get 20 Percentage split
            var threshold = (int)Math.Ceiling(Students.Count * 0.2);
            var grades = Students.OrderByDescending(s => s.AverageGrade).Select(s => s.AverageGrade).ToList();

            for (int level = 0; level < 5; level++)
            {
                if(grades[(threshold * (level + 1)) - 1] <= averageGrade)
                {
                    switch(level){
                        case 0: return 'A';
                        case 1: return 'B';
                        case 2: return 'C';
                        case 3: return 'D';
                    }
                }
            }
            return 'F';
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace AndroidTestApp.Models
{
    public class StudentsTest
    {
        public int Id { get; set; }

        public int? TestId { get; set; }

        public int StudentId { get; set; }

        public int? Result { get; set; }

        public int AttemtsLeft { get; set; }

        public DateTime? Time { get; set; }

        public static implicit operator StudentsTest(List<StudentsTest> v)
        {
            throw new NotImplementedException();
        }
    }
}

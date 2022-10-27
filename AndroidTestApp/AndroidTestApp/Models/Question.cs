using System;
using System.Collections.Generic;
using System.Text;

namespace AndroidTestApp.Models
{
    public class Question
    {
        public int Id { get; set; }

        public int? TestId { get; set; }

        public string Name { get; set; } = null!;

#nullable enable
        public byte[]? Picture { get; set; }

        public int QuestionTypeId { get; set; }

        public int Value { get; set; }
    }
}

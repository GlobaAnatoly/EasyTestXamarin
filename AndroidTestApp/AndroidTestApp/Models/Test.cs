using System;
using System.Collections.Generic;
using System.Text;

namespace AndroidTestApp.Models
{
    public class Test
    {
        public int Id { get; set; }

        public int TeacherId { get; set; }

        public int SubjectId { get; set; }

        public string Name { get; set; } = null!;
    }
}

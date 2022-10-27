using System;
using System.Collections.Generic;
using System.Text;

namespace AndroidTestApp.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string StudentName { get; set; } = null!;

        public string StudentLogin { get; set; } = null!;

        public byte[] StudentPassword { get; set; } = null!;
#nullable enable
        public virtual string? StudentPasswordStr { get; set; } = null!;
    }
}

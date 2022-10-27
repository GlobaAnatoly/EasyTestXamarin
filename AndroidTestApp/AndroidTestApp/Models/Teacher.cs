using System;
using System.Collections.Generic;
using System.Text;

namespace AndroidTestApp.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        public string TeacherName { get; set; } = null!;

        public string TeacherLogin { get; set; } = null!;

        public byte[] TeacherPassword { get; set; } = null!;
        #nullable enable
        public virtual string? TeacherPasswordStr { get; set; } = null!;
    }
}

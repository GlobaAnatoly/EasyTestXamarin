using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AndroidTestApp.Models
{
    public class AnswerVariant
    {
        public int Id { get; set; }

        public int IdQuestion { get; set; }

        public string Name { get; set; } = null!;

        public bool IsCorrect { get; set; }

        public static explicit operator AnswerVariant(Task<List<AnswerVariant>> v)
        {
            throw new NotImplementedException();
        }
    }
}

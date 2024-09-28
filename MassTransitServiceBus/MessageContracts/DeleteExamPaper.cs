using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageContracts
{
    public record DeleteExamPaper
    {
        public required string ExamName { get; init; }
    }
}

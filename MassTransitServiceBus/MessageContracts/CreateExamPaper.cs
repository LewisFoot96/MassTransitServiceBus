namespace MessageContracts
{
    public record CreateExamPaper
    {
        public required string ExamName { get; init; }
    }
}

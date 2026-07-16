namespace Bioingenieria.Models;

public enum Semester
{
    FirstHalf,
    SecondHalf
}

public static class SemesterExtensions
{
    public static Semester Current => DateTime.Today.Month <= 6 ? Semester.FirstHalf : Semester.SecondHalf;

    public static (int Start, int End) GetWeekRange(this Semester semester)
    {
        return semester == Semester.FirstHalf ? (1, 24) : (25, 48);
    }
}

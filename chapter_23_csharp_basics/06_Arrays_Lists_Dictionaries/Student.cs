public class Student
{
    public string? Name { get; set; }
    public string? Course { get; set; }

    public Student(string name, string course)
    {
        Name = name;
        Course = course;
    }
}
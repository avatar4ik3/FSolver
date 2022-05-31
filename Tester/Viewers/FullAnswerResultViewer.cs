namespace Tester;

public class FullAnswerConsoleResultViewer : IResultViewer
{
    public FullAnswerConsoleResultViewer(SolverDirector director)
    {
        Director = director;
    }

    public SolverDirector Director { get; }

    public void View()
    {
        var results = Director.Solve();
        Console.WriteLine("summary:");
        foreach (var result in results.values)
        {
            result.Elements.Sort((o1, o2) => o1.Number.CompareTo(o2.Number));
            foreach (var elem in result.Elements)
            {
                Console.WriteLine($"{result.ResultName} {elem.Number}");
            }
        }
        Console.WriteLine("details:");
        foreach (var result in results.values)
        {
            foreach (var elem in result.Elements)
            {
                Console.WriteLine($"{result.ResultName} -> {elem.Number}");
                Console.WriteLine($"{elem.Model}");
            }
        }
    }
}
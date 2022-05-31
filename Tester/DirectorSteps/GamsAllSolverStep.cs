using GAMS;

namespace Tester.DirectorSteps;

public class GamsAllSolverStep : GamsSolverStep<GamsSolver>
{
    public GamsAllSolverStep(Config config, List<Model> models, GAMSWorkspace ws) : base(config, models, ws, "Gams Solver")
    {
    }

    public override ResultsElements Step()
    {
        var list = new List<ResultElement>();
        Parallel.ForEach(Enumerable.Range(0, Models.Count()), i => {
            Console.WriteLine($"{Name} : {i} solving.");
            var solver = new GamsSolver(Config,Models[i],WS, WS.AddJobFromFile(Config.FileName));
            var result = solver.Solve();
            list.Add(new(i,result));
            Console.WriteLine($"{Name} : {i} solved.");
        });
        var output = new ResultsElements(Name);
        output.Elements.AddRange(list);
        return output;
    }
}
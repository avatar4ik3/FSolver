namespace Tester.DirectorSteps;

public class GurobiSolverStep : AlgSolverStep<GurobiSolver>
{
    public GurobiSolverStep(Config config, List<Model> models) : base("Gurobi Alg 1", config, models)
    {
    }

    public override GurobiSolver GetInstance(int index)
    {
        return new GurobiSolver(Config,Models[index]);
    }
}
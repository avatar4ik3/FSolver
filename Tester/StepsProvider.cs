using GAMS;
using Tester.DirectorSteps;

namespace Tester;

public class StepsProvider{
    private List<SolverStep> _steps = new List<SolverStep>(); 

    public StepsProvider(GAMSWorkspace ws,Config config, RandomModelDataProvider dataProvider)
    {
        //var models = dataProvider.Provide(config.Itterations);
    }

    public List<SolverStep> GetSteps(string[] names){
        return _steps.Where(step => names.Contains(step.Name)).ToList();
    }
}
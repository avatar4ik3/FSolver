using GAMS;
using Tester.DirectorSteps;

namespace Tester;

public class StepsProvider{
    private List<SolverStep> _steps = new List<SolverStep>(); 

    public StepsProvider(GAMSWorkspace ws,Config config, RandomModelDataProvider dataProvider)
    {
        var models = dataProvider.Provide(config.Itterations);
        //_steps.Add(new GurobiSolverStep(config,models));
        _steps.Add(new GamsAllSolverStep(config,models,ws));
    }

    public List<SolverStep> GetSteps(){
        return _steps;
    }
}
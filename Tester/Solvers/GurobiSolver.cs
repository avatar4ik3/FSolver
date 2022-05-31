using Gurobi;
namespace Tester;

public class GurobiSolver : Solver
{
    public GurobiSolver(Config config, Model model) : base(config, model)
    {
        _xs = new GRBVar[config.VertexCount][];
        for (int i = 0; i < config.VertexCount; ++i)
        {
            _xs[i] = new GRBVar[config.VertexCount];
        }
    }

    private GRBVar[][] _xs;
    public override ResultModel Solve()
    {
        try
        {

            // Create an empty environment, set options and start
            using GRBEnv env = new GRBEnv(true);
            env.Set("LogFile", "mip1.log");
            env.Start();

            // Create empty model
            using GRBModel model = new GRBModel(env);
            for (int i = 0; i < Config.VertexCount; ++i)
            {
                for (int j = 0; j < Config.VertexCount; ++j)
                {
                    _xs[i][j] = model.AddVar(0.0, 1.0, 0.0, GRB.BINARY, $"x[{i}][{j}]");
                }
            }

            GRBLinExpr all = 0.0;
            for (int i = 0; i < Config.VertexCount; ++i)
            {
                GRBLinExpr ntot = 0.0;
                for (int j = 0; j < Config.VertexCount; ++j)
                {
                    if (Model.Graph[i][j] is false)
                    {
                        ntot.Add(1 - _xs[i][j]);
                    }
                    else
                    {
                        ntot.Add(_xs[i][j]);
                    }
                }
                all.Add(ntot);
            }
            model.SetObjective(all, GRB.MINIMIZE);

            //добавляем первое ограничение
            for (int i = 0; i < Config.VertexCount; ++i)
            {
                for (int j = 0; j < Config.VertexCount; ++j)
                {
                    for (int k = 0; k < Config.VertexCount; ++k)
                    {
                        if ((i == j && j == k && i == k) is false)
                        {
                            model.AddConstr(_xs[i][k] <= _xs[i][j] + _xs[j][k], $"e_1_i:{i}_j:{j}_k:{k}");
                        }
                    }
                }
            }

            //e2
            for (int i = 0; i < Config.VertexCount; ++i)
            {
                GRBLinExpr ntot = 0.0;
                for (int j = 0; j < Config.VertexCount; ++j)
                {
                    if (i != j)
                    {
                        ntot.AddTerm(1, _xs[i][j]);
                    }
                }
                model.AddConstr(ntot >= Config.VertexCount - Config.S, $"e_2_i:{i}");
            }




            // Optimize model
            model.Optimize();
            foreach (var constr in model.GetConstrs())
            {
                Console.WriteLine($"1:{constr.ConstrName}");
                Console.WriteLine($"->2:{constr.CTag}");
                Console.WriteLine($"->3:{constr.Sense}"); ;
            }
            if (model.Status == 3)
            {
                Console.WriteLine("Infeasiable");
            }
            return ToResultModel();



            //Console.WriteLine("Obj: " + model.ObjVal);

            // Dispose of model and env


        }
        catch (GRBException e)
        {
            Console.WriteLine("Error code: " + e.ErrorCode + ". " + e.Message);
        }

        return null!;
    }

    private ResultModel ToResultModel()
    {
        var result = new ResultModel(Config.VertexCount);
        for (int i = 0; i < Config.VertexCount; ++i)
        {
            for (int j = 0; j < Config.VertexCount; ++j)
            {
                result.X[i][j] = _xs[i][j].X == 1.0 ? true : false;
            }
        }
        return result;
    }


    private void ShowResult()
    {
        for (int i = 0; i < Config.VertexCount; ++i)
        {
            for (int j = 0; j < Config.VertexCount; ++j)
            {
                Console.Write(_xs[i][j].VarName);
            }
            Console.WriteLine();
        }
    }
}
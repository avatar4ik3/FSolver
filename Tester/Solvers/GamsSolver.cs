using GAMS;

namespace Tester;

public class GamsSolver : Solver
{
    public GamsSolver(Config config, Model model, GAMSWorkspace ws, GAMSJob job) : base(config, model)
    {
        this.WS = ws;
        Job = job;
        db = WS.AddDatabase();

        GAMSSet g_i = db.AddSet("i", 1, "a");
        GAMSParameter g_s = db.AddParameter("s", "a");
        GAMSParameter g_n = db.AddParameter("n", "a");

        GAMSParameter g_G = db.AddParameter("G", 2, "a");

        g_s.AddRecord().Value = model.S;
        g_n.AddRecord().Value = config.VertexCount;

        foreach (var i in Enumerable.Range(1, config.VertexCount ))
        {
            g_i.AddRecord(i.ToString());
        }

        foreach (var i in Enumerable.Range(1, config.VertexCount ))
        {
            foreach (var j in Enumerable.Range(1, config.VertexCount ))
            {
                g_G.AddRecord(i.ToString(), j.ToString()).Value = Model.Graph[i - 1][j - 1] is true ? 1 : 0;
            }
        }
    }

    public GAMSWorkspace WS { get; private set; }
    public GAMSJob Job { get; }
    public GAMSDatabase db { get; private set; }
    public override ResultModel Solve()
    {
        GAMSOptions gOptions = WS.AddOptions();
        gOptions.Defines.Add("gdxincname", db.Name);
        Job.Run(gOptions, db);

        var result = new ResultModel(Config.VertexCount);

        int GamsSetElementToIntegerValue(string setElement)
        {
            return int.Parse(setElement);
        }

        bool ConvertDoubleToBool(double value)
        {
            return Math.Abs(value - 1) <= 1e-5 ? true : false;
        }
        foreach (GAMSVariableRecord elem in Job.OutDB.GetVariable("x"))
        {
            result.X
                [GamsSetElementToIntegerValue(elem.Keys[0]) - 1]
                [GamsSetElementToIntegerValue(elem.Keys[1]) - 1] = 
                ConvertDoubleToBool(elem.Level);
        }

        foreach(GAMSVariableRecord elem in Job.OutDB.GetVariable("d")){
            result.D = (int) elem.Level;
        }
        return result;
    }
}
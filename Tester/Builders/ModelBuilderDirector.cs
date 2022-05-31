using System;
using CH.Combinations;
using System.Collections;
using System.Linq;
namespace Tester;


public class ModelBuilderDirector
{
    public Config Config { get; }
    public Random Random { get; }

    public ModelBuilderDirector(Config config, Random random)
    {
        Config = config;
        Random = random;
    }

    public Model BuildRandomModel()
    {

        var model = new Model();


        var s = Config.S;

        model.S = s;
        var n = Config.VertexCount;

        model.Graph = new bool[n][];
        for (int i = 0; i < n; ++i)
        {
            model.Graph[i] = new bool[n];
        }

        foreach (var edge in new Combinations<int>(Enumerable.Range(0, n).ToArray(), 2))
        {
            if (model.Graph[edge[0]][edge[1]] is true)
            {
                continue;
            }

            if (Random.NextDouble() <= Config.P)
            {
                model.Graph[edge[0]][edge[1]] = true;
                model.Graph[edge[1]][edge[0]] = true;

            }
        }
        return model;
    }

    public Model BuildConcreteModel()
    {
         var model = new Model();


        var s = 3;

        model.S = s;
        var n = 3;

        model.Graph = new bool[n][];
        for (int i = 0; i < n; ++i)
        {
            model.Graph[i] = new bool[n];
        }

       
       model.Graph[0][0] = false;
       model.Graph[0][1] = true;
       model.Graph[0][2] = true;
       model.Graph[1][0] = true;
       model.Graph[1][1] = false;
       model.Graph[1][2] = true;
       model.Graph[2][0] = true;
       model.Graph[2][1] = true;
       model.Graph[2][2] = false;




        return model;
    }
}
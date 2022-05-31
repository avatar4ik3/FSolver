using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using Tester;
using Microsoft.Extensions.Configuration;

namespace Tester;

public class Programm
{
    public static void Main(string[] args)
    {
        // using  FileStream filestream = new FileStream("out.txt", FileMode.Create);
        // using var streamwriter = new StreamWriter(filestream);
        // streamwriter.AutoFlush = true;
        // Console.SetOut(streamwriter);
        // Console.SetError(streamwriter);

        // IServiceProvider provider = BuildServiceProvider();
        // var dir = provider.GetService<FullAnswerConsoleResultViewer>();
        // dir!.View();

        // var config = new Config(){
        //     RandomSeed = 10,
        //     S = 3,
        //     VertexCount = 10,
        //     P = 1D/2D,
        //     Itterations = 0
        // };
        // var builder = new ModelBuilderDirector(config,new Random(123));
        // var model1 = builder.BuildRandomModel();
        // var model2 = builder.BuildRandomModel();

        // Console.WriteLine(model1);
        // Console.WriteLine(model2);
        var solver = new GurobiSolver(null!,null!);
        solver.Solve();
    }

    private static IServiceProvider BuildServiceProvider()
    {
        IServiceCollection collection = new ServiceCollection();
        IConfiguration configuration = new ConfigurationBuilder()
        .AddJsonFile("appSettings.json", optional: false)
        .Build();
        Config config = configuration.Get<Config>();
        collection.AddSingleton<Config>(config);
        collection.AddSingleton<ConsoleResultViewer>();
        Random rnd = null!;
        if (config.RandomSeed == -1)
        {
            rnd = new Random();
        }
        else
        {
            rnd = new Random(config.RandomSeed);
        }
        collection.AddSingleton<Random>(rnd);
        collection.AddSingleton<SolverDirector>();
        collection.AddSingleton<ModelBuilderDirector>();
        collection.AddSingleton<FullAnswerConsoleResultViewer>();
        collection.AddSingleton<RandomModelDataProvider>();
        collection.AddSingleton<StepsProvider>();
        return collection.BuildServiceProvider();
    }
}


public class Config
{
    public int RandomSeed { get; set; }

    public int S {get;set;}

    public int VertexCount {get;set;}

    public double P {get;set;}

    public int Itterations {get;set;}
}




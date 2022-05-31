namespace Tester;

public class RandomModelDataProvider{
    
    public RandomModelDataProvider(ModelBuilderDirector director){
        Director = director;
    }

    public ModelBuilderDirector Director { get; }

    public List<Model> Provide(int count){
        var result = new List<Model>();
        foreach(var i in Enumerable.Range(0,count)){
            var model = Director.BuildRandomModel();
            Console.WriteLine($"{i} : {model}"); 
            result.Add(model);
        }
        return result;
    }

    
}
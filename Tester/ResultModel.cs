using System.Text;

namespace Tester;
public class ResultModel{
    private readonly int count;
    public bool[][] X {get;set;} = null!;

    public int D {get;set;}
    public ResultModel(int count){ 
        X = new bool[count][];
        for(int i = 0; i < count;++i){
            X[i] = new bool[count];
        }

        this.count = count;
    }

    public override string? ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"D : {D}");
        sb.AppendLine("X:");

        for(int i = 0;i < count; ++i){
            for(int j = 0;j < count;++j){
                sb.Append($"{(X[i][j] == true ? 1 : 0)}, ");
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }
}
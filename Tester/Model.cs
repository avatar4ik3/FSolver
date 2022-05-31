using System.Text;

namespace Tester;
public class Model{
    public bool[][] Graph {get;internal set;} = null!;
    public int S{get;internal set;}

    
    public Model(){

    }

    public override string? ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"S: {S}");
        for(int i = 0;i < Graph.Length; ++i){
            for(int j =0 ;j < Graph[0].Length;++j){
                sb.Append($"{(Graph[i][j] is true ? 1 : 0)}, ");
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }
}
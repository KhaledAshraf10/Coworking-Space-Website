namespace ChartExample.Models.Chart;

public class Dataset


{
    public string label { get; set; }
    public List<int> data { get; set; }
    public List<string> backgroundColor { get; set; }
    public List<string> borderColor { get; set; }
    public int borderWidth { get; set; }
    public string yAxisID { get; set; }
    public string xAxisID { get; set; }
}
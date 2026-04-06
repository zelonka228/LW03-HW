namespace LW03_HW.Core;


public class ReportEventArgs
{
    public AnalysisResult Result { get; set; }

    public ReportEventArgs(AnalysisResult result)
    {
        Result = result;
    }
}

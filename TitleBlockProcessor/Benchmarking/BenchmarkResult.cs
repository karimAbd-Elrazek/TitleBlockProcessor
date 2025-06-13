using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitleBlockProcessor.Benchmarking;
public class BenchmarkResult
{
    public string MethodName { get; set; }
    public TimeSpan TotalTime { get; set; }
    public int ProcessedSheets { get; set; }
    public int SuccessfulSheets { get; set; }
    public TimeSpan AverageTimePerSheet { get; set; }
    public long MemoryUsage { get; set; }
    public string AdditionalInfo { get; set; }

    public double TotalMilliseconds => TotalTime.TotalMilliseconds;
    public double AverageMillisecondsPerSheet => AverageTimePerSheet.TotalMilliseconds;
    public double MemoryUsageMB => MemoryUsage / (1024.0 * 1024.0);
}

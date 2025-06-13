using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitleBlockProcessor.Core.Managers;
using TitleBlockProcessor.Core.Models;
using TitleBlockProcessor.Core.Services;

namespace TitleBlockProcessor.Benchmarking;
public class TitleBlockPerformanceService
{
    public List<BenchmarkResult> BenchmarkResults { get; } = new();

    private readonly List<ViewSheet> _testSheets;
    private readonly ViewSheetsManager _viewSheetsManager;
    private readonly SheetTitleBlockService _sheetTitleBlockService;
    private readonly ViewSheetsService _viewSheetsService;

    public TitleBlockPerformanceService(
        List<ViewSheet> sheets,
        ViewSheetsManager viewSheetsManager,
        SheetTitleBlockService sheetTitleBlockService,
        ViewSheetsService viewSheetsService)
    {
        _testSheets = sheets ?? throw new ArgumentNullException(nameof(sheets));
        _viewSheetsManager = viewSheetsManager ?? throw new ArgumentNullException(nameof(viewSheetsManager));
        _sheetTitleBlockService = sheetTitleBlockService;
        _viewSheetsService = viewSheetsService;
    }

    public List<BenchmarkResult> RunAllBenchmarks()
    {
        BenchmarkResults.Clear();
        BenchmarkResults.Add(RunBenchmark("Method 1: FilteredElementCollector on each sheet", _viewSheetsManager.GetTitleBlockMethod1));
        BenchmarkResults.Add(RunBenchmark("Method 2:GetDependentElements() for each sheet (no caching)", _viewSheetsManager.GetTitleBlockMethod2));
        BenchmarkResults.Add(RunBenchmark("Method 3: GetDependentElements() with caching", _viewSheetsManager.GetTitleBlockMethod3, includeExtraInfo: true));
        return BenchmarkResults;
    }

    private BenchmarkResult RunBenchmark(string methodName, Func<ViewSheet,TitleBlockData> method, bool includeExtraInfo = false)
    {
        ForceGC();

        var stopwatch = Stopwatch.StartNew();
        int successCount = 0;

        foreach (var sheet in _testSheets)
        {
            try
            {
                method(sheet);
                successCount++;
            }
            catch
            {
                // Silent catch
            }
        }

        stopwatch.Stop();

        return new BenchmarkResult
        {
            MethodName = methodName,
            TotalTime = stopwatch.Elapsed,
            ProcessedSheets = _testSheets.Count,
            SuccessfulSheets = successCount,
            AverageTimePerSheet = TimeSpan.FromMilliseconds(stopwatch.Elapsed.TotalMilliseconds / _testSheets.Count),
            MemoryUsage = GC.GetTotalMemory(false),
            AdditionalInfo = includeExtraInfo
                ? $"Cached Title Blocks: {_sheetTitleBlockService?.TitleBlockIdToTitleBlockData?.Count ?? 0}, Mapped Sheets: {_viewSheetsService?.SheetToTitleBlockMap?.Count ?? 0}"
                : null
        };
    }

    private static void ForceGC()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
    }

    public void WriteBenchmarkResultsToDesktop(string fileName = "BenchmarkResults.txt")
    {
        var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        var fullPath = Path.Combine(desktopPath, fileName);

        var lines = BenchmarkResults.Select(result => $"""
            ====================================
            Method Name:              {result.MethodName}
            Total Time:               {result.TotalTime}
            Processed Sheets:         {result.ProcessedSheets}
            Successful Sheets:        {result.SuccessfulSheets}
            Average Time per Sheet:   {result.AverageTimePerSheet}
            Total ms:                 {result.TotalMilliseconds:F2} ms
            Avg ms per Sheet:         {result.AverageMillisecondsPerSheet:F2} ms
            Memory Usage:             {result.MemoryUsageMB:F2} MB
            Additional Info:          {result.AdditionalInfo}
            ====================================
            """);

        File.WriteAllText(fullPath, string.Join(Environment.NewLine, lines));
    }
}




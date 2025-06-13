using Autodesk.Revit.Attributes;
using Nice3point.Revit.Toolkit.External;
using TitleBlockProcessor.Core.Managers;
using TitleBlockProcessor.Core.Services;
using TitleBlockProcessor.Benchmarking;
using Autodesk.Revit.UI;

namespace TitleBlockProcessor.Commands;
/// <summary>
///     External command entry point invoked from the Revit interface
/// </summary>
[UsedImplicitly]
[Transaction(TransactionMode.Manual)]
public class StartupCommand : ExternalCommand
{
    public override void Execute()
    {
        try
        {

            //Dependances
            var sheetTitleService = new SheetTitleBlockService(Document);
            var viewSheetService = new ViewSheetsService(Document, sheetTitleService);
            var sheetManager = new ViewSheetsManager(Document, sheetTitleService, viewSheetService);
            var sheets = sheetManager.GetAllSheets();
            var TitleBlockPerformanceService = new TitleBlockPerformanceService(sheets, sheetManager, sheetTitleService, viewSheetService);
          
            
            //Task
            TitleBlockPerformanceService.RunAllBenchmarks();
            TitleBlockPerformanceService.WriteBenchmarkResultsToDesktop();
            TaskDialog.Show("info", "Done!");

        }
        catch (Exception ex)
        {
            TaskDialog.Show("Error", "Error happened !");
        }
    }
}
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitleBlockProcessor.Core.Models;
using TitleBlockProcessor.Core.Services;

namespace TitleBlockProcessor.Core.Managers;
public class ViewSheetsManager
{
    Document _doc;
    private SheetTitleBlockService _sheetTitleBlockService;
     ViewSheetsService _viewSheetsService;

    public ViewSheetsManager(Document document, SheetTitleBlockService sheetTitleBlockService, ViewSheetsService viewSheetsService)
    {
        _doc = document;
        _sheetTitleBlockService = sheetTitleBlockService;
        _viewSheetsService = viewSheetsService;
    }

    public  List<ViewSheet> GetAllSheets()
    {
        return new FilteredElementCollector(_doc)
            .OfClass(typeof(ViewSheet))
            .Cast<ViewSheet>()
            .Where(sheet => !sheet.IsTemplate) 
            .ToList();
    }

    public TitleBlockData GetTitleBlockMethod1(ViewSheet sheet)
    {
        var titleBlock = new FilteredElementCollector(_doc, sheet.Id)
            .OfCategory(BuiltInCategory.OST_TitleBlocks)
            .FirstOrDefault() as FamilyInstance;
        return titleBlock == null ? null : new TitleBlockData(titleBlock);
    }

    public TitleBlockData GetTitleBlockMethod2(ViewSheet sheet)
    {
        var titleBlock = _sheetTitleBlockService.GetTitleBlock(sheet);
        if (titleBlock == null) return null;
        return titleBlock == null ? null : new TitleBlockData(titleBlock);
    }

    //caching for future using [get data one time and use all session ]
    public TitleBlockData GetTitleBlockMethod3(ViewSheet sheet)
    {
        return _viewSheetsService.GetTitleBlockData(sheet);
    }

}

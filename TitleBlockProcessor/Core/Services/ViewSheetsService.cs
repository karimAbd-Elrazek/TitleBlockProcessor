using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitleBlockProcessor.Core.Models;

namespace TitleBlockProcessor.Core.Services;
public class ViewSheetsService
{
   public Dictionary<ElementId, ElementId> SheetToTitleBlockMap { get; private set; }
    Document _doc;
    SheetTitleBlockService _sheetTitleBlockService;
    public ViewSheetsService(Document document, SheetTitleBlockService sheetTitleBlockService)
    {
        _doc = document;
        _sheetTitleBlockService = sheetTitleBlockService;
        SheetToTitleBlockMap = new Dictionary<ElementId, ElementId>();
    }

    public TitleBlockData GetTitleBlockData(ViewSheet sheet)
    {
        ElementId sheetId = sheet.Id;
        ElementId titleBlockId;

        if (SheetToTitleBlockMap.ContainsKey(sheetId))
        {
            titleBlockId = SheetToTitleBlockMap[sheetId];
        }
        else
        {
            var titleBlock = _sheetTitleBlockService.GetTitleBlock(sheet);
            titleBlockId = titleBlock == null ? null : titleBlock.Id;
            SheetToTitleBlockMap.Add(sheetId, titleBlockId);
            
        }
        return _sheetTitleBlockService.TitleBlockIdToTitleBlockData.TryGetValue(titleBlockId, out TitleBlockData data) ? data : null;
    }



}

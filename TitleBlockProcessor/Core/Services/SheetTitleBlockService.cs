using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitleBlockProcessor.Core.Models;

namespace TitleBlockProcessor.Core.Services;
public class SheetTitleBlockService
{
    public Dictionary<ElementId, TitleBlockData> TitleBlockIdToTitleBlockData { get; private set; }
    private Document _doc;

    public SheetTitleBlockService(Document document)
	{
		_doc= document;
        CacheAllTitleBlocks();
    }


    private void CacheAllTitleBlocks()
    {
        TitleBlockIdToTitleBlockData = new Dictionary<ElementId, TitleBlockData>();
        var titleBlocks = new FilteredElementCollector(_doc)
                                                  .OfCategory(BuiltInCategory.OST_TitleBlocks)
                                                  .WhereElementIsNotElementType()
                                                  .Cast<FamilyInstance>();

        foreach (FamilyInstance tb in titleBlocks)
        {
            TitleBlockIdToTitleBlockData[tb.Id] = new TitleBlockData(tb);
        }
    }

    public FamilyInstance GetTitleBlock(ViewSheet sheet)
    {
        var dependentElements = sheet.GetDependentElements(null);

        foreach (ElementId elemId in dependentElements)
        {
            Element elem = _doc.GetElement(elemId);
            if (elem is FamilyInstance fi &&
                fi.Category.Id.IntegerValue == (int)BuiltInCategory.OST_TitleBlocks)
            {
                return fi;
            }
        }
        return null;
    }
}

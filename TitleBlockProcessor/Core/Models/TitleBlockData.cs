using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitleBlockProcessor.Core.Models;
public class TitleBlockData
{
    //SomePop
    public bool IsAnalyzed { get; set; }
    public TitleBlockData(FamilyInstance titleBlock)
    {
        if (titleBlock == null)
        {
            //Handle sheet with no TitleBlock..
            IsAnalyzed = false;
        }
        else
        {
            // Get some Parameters
            // make some  analysis with parameters
            //  fill props with results
            IsAnalyzed = true;
        }

    }
}

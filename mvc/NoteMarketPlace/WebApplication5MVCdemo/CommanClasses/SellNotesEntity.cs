using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoteMarketPlace.CommanClasses
{
    public class SellNotesEntity
    {
        public bool CheckNoteStatusPaidOrNot(string IsPaidOrNot)
        {
            bool Result = false;
            if (IsPaidOrNot.ToString() == "true")
            {
                Result = true;
            }
            else if (IsPaidOrNot.ToString() == "false")
            {
                Result = false;
            }
            return Result;
        }
    }
}
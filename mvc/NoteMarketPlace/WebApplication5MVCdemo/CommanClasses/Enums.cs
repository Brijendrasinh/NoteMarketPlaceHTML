using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoteMarketPlace.CommanClasses
{
    public class Enums
    {
        public enum UserRoleId
        {
            SuperAdmin = 1,
            Admin = 2,
            Member = 3
        }

        public enum ReferenceNoteStatus
        {
            Draft = 3,
            SubmittedForReview = 4,
            InReview = 5,
            Approved = 6,
            Rejected = 7,
            Removed = 8
        }
    }
}
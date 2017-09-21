using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Model
{
    public class Constants
    {
        public static class EmailTemplates
        {
            public const string NewConcession = "NewConcession";
            public const string ConcessionApproved = "Approved";
            public const string ConcessionApprovedWithChanges = "ApprovedWithChanges";
            public const string ConcessionDeclined = "Declined";

        }
        public static class Roles
        {
            public const string Requestor = "Requestor";
            public const string SuiteHead  = "Suite Head";
            public const string BCM = "BCM";
            public const string PCM = "PCM";
            public const string HeadOffice = "Head Office";
        }
        public  enum ApprovalStep
        {
             BCMApproval,
             PCMApproval,
             RequestorApproval
        }
      
      
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.DTO.Enums
{

    public enum ConcessionStatuses
    {
        Pending = 1,
        Approved = 2,
        ApprovedWithChanges = 3,
        Declined = 4,
        Removed = 5,
    }

    public enum ConcessionSubStatuses
    {
        BCMPending = 1,
        BCMApproved = 2,
        BCMDeclined = 3,
        PCMPending = 4,
        PCMApproved = 5,
        PCMDeclined = 6,
        HOPending = 7,
        HOApproved = 8,
        HODeclined =9,
        RequestorAccepetedChanges=10,
        RequestorDeclinedChanges = 11,
    }

    public enum Roles
    {
        Requestor = 1,
        SuiteHead = 2,
        BCM = 3,
        PCM = 4,
        HeadOffice = 5,

    }
}

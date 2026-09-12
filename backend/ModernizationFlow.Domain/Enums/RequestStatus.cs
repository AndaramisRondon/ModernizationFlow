using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernizationFlow.Domain.Enums;

public enum RequestStatus
{
    Draft = 1,
    UnderReview = 2,
    Approved = 3,
    Rejected = 4
}
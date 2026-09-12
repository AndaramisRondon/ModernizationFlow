using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernizationFlow.Application.Requests.Update;

public sealed record UpdateRequestCommand(
    Guid Id,
    string Title,
    string Description,
    decimal Amount);


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernizationFlow.Application.Requests.Create;

public sealed record CreateRequestCommand(
    string Title,
    string Description,
    decimal Amount);



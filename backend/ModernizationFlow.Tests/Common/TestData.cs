using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Globalization;

namespace ModernizationFlow.Tests.Common;

public static class TestData
{
    private static readonly CultureInfo PtBr =
        new("pt-BR");

    public static string NewTitle() =>
        $"Teste {DateTime.Now.ToString("dd MMMM yyyy HH:mm:ss", PtBr)}";

    public static string DefaultDescription =>
        "Descrição de teste";

    public static decimal DefaultAmount =>
        1000m;
}


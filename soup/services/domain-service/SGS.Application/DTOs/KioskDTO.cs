using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Application.DataTransferObjects;
public class CreateKioskArg
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phonenumber { get; set; } = string.Empty;
}

public class UpdateKioskArg
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phonenumber { get; set; } = string.Empty;
}



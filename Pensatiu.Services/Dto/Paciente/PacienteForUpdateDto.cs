﻿using Pensatiu.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pensatiu.Services.Dto.Paciente
{
    public class PacienteForUpdateDto: IDtoForUpdate
    {
        public string Nome { get; set; }
    }
}

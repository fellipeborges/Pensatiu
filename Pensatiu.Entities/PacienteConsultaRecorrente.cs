﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Pensatiu.Entities
{
    public class PacienteConsultaRecorrente
    {
        public int Id { get; set; }

        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }

        public int ConsultorioId { get; set; }
        public Consultorio Consultorio { get; set; }

        [Required]
        public PacienteConsultaRecorrenteDiaDaSemanaEnum DiaSemana { get; set; }

        [Required]
        public TimeSpan Hora { get; set; }
    }

    public enum PacienteConsultaRecorrenteDiaDaSemanaEnum
    {
        Domingo = 1,
        SegundaFeira = 2,
        TercaFeira = 3,
        QuartaFeira = 4,
        QuintaFeira = 5,
        SextaFeira = 6,
        Sabado = 7
    }
}
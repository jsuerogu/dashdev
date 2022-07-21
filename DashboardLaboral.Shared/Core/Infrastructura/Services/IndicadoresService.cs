using DashboarLaboral.Core.Aplicacion.Contratos.Repositorios;
using DashboarLaboral.Core.Infrastructura.Repositorios;
using DashboarLaboral.Data;
using DashboarLaboral.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DashboarLaboral.Core.Infrastructura.Services
{
    public class IndicadoresService : IIndicadoresService
    {
        private readonly insitedb context;
        private readonly DataLaboranHoy dataLaboranHoy;
        private readonly DataNoLaboranHoy dataNoLaboranHoy;
        private readonly DataTotalEmpl dataTotalEmpl;
        private readonly DataInasistencias dataInasistencias;
        private readonly DataTardanzas dataTardanzas;
        private readonly DataHorasExtras dataTeletrabajo;
        private readonly DataOnPremise dataOnPremise;
        private readonly DataPresentes dataPresentes;
        private readonly DataOffPremise dataOffPremise;
        private readonly DataSalidaFueraHorario dataSalidaFueraHorario;
        private readonly DataAusenciaJustificada dataAusenciaJustificada;
        private readonly DataCondicionRiesgo dataCondicionRiesgo;
        private readonly DataCuarentena dataCuarentena;
        private readonly DataNoTocaTrabajar dataNoTocaTrabajar;
        private readonly DataAsistenciaNoJustificada dataAsistenciaNoJustificada;

        public IndicadoresService(insitedb context, DataLaboranHoy dataLaboranHoy,
            DataNoLaboranHoy dataNoLaboranHoy, DataTotalEmpl dataTotalEmpl, DataInasistencias dataInasistencias, DataTardanzas dataTardanzas, DataHorasExtras dataTeletrabajo, DataOnPremise dataOnPremise, DataPresentes dataPresentes, DataOffPremise dataOffPremise, DataSalidaFueraHorario dataSalidaFueraHorario, DataAusenciaJustificada dataAusenciaJustificada, DataCondicionRiesgo dataCondicionRiesgo, DataCuarentena dataCuarentena, DataNoTocaTrabajar dataNoTocaTrabajar, DataAsistenciaNoJustificada dataAsistenciaNoJustificada)
        {
            this.context = context;
            this.dataLaboranHoy = dataLaboranHoy;
            this.dataNoLaboranHoy = dataNoLaboranHoy;
            this.dataTotalEmpl = dataTotalEmpl;
            this.dataInasistencias = dataInasistencias;
            this.dataTardanzas = dataTardanzas;
            this.dataTeletrabajo = dataTeletrabajo;
            this.dataOnPremise = dataOnPremise;
            this.dataPresentes = dataPresentes;
            this.dataOffPremise = dataOffPremise;
            this.dataSalidaFueraHorario = dataSalidaFueraHorario;
            this.dataAusenciaJustificada = dataAusenciaJustificada;
            this.dataCondicionRiesgo = dataCondicionRiesgo;
            this.dataCuarentena = dataCuarentena;
            this.dataNoTocaTrabajar = dataNoTocaTrabajar;
            this.dataAsistenciaNoJustificada = dataAsistenciaNoJustificada;
        }

        public async Task<Dictionary<string, IndicadorModel>> CargarIndicadoresExtendidos(DateTime fechaHoy)
        {
            var indicadores = new Dictionary<string, IndicadorModel>
            {
                { "Presentes", await dataPresentes .ObtenerModel(fechaHoy) },
                { "Inasistencias", await dataInasistencias .ObtenerModel(fechaHoy) },
                { "SalidasFueraHorario", await dataSalidaFueraHorario.ObtenerModel(fechaHoy)},
                { "HorasExtras",  await dataTeletrabajo .ObtenerModel(fechaHoy)},
                { "Tardanzas", await dataTardanzas.ObtenerModel(fechaHoy) },
                { "AsistenciaNoJustificada", await dataAsistenciaNoJustificada.ObtenerModel(fechaHoy) }


            };
            return indicadores;
        }

        public async Task<Dictionary<string, IndicadorModel>> CargarIndicadoresSimples(DateTime fechaHoy)
        {
            var indicadores = new Dictionary<string, IndicadorModel>
            {
                { "TotalEmpl", await dataTotalEmpl .ObtenerModel(fechaHoy) },
                { "LaboranHoy", await dataLaboranHoy.ObtenerModel(fechaHoy) },
                { "NoLaboranHoy", await dataNoLaboranHoy.ObtenerModel(fechaHoy)},
                { "OnPremise", await dataOnPremise .ObtenerModel(fechaHoy) },
                { "OffPremise", await dataOffPremise.ObtenerModel(fechaHoy) },
                { "CondRiesgo", await  dataCondicionRiesgo.ObtenerModel(fechaHoy) },
                { "Cuarentena", await dataCuarentena.ObtenerModel(fechaHoy) },
                { "AusenciaJust", await dataAusenciaJustificada.ObtenerModel(fechaHoy)},
                { "NoTocaTrabajar", await dataNoTocaTrabajar.ObtenerModel(fechaHoy) }
                
            };
            return indicadores;
        }


    }
}

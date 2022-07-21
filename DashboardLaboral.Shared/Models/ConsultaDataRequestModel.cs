using System.Collections.Generic;

namespace DashboarLaboral.Models
{
    public class ConsultaDataRequestModel
    {
        public FiltroModel Filtro { get; set; }
        public string Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public string ExtraData { get; set; }

    }

    public class PaginateDataResponseModel<TData>
    {
        public List<TData> Data { get; set; }
        public string Draw { get; set; }
        public int RecordsFiltered { get; set; }
        public int RecordsTotal { get; set; }
    }

}

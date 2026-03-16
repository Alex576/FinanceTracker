using MasterData.Data.DBModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MasterData.Data.Models
{
    public class FinanceModel
    {
        public int Id { get; set; }
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public DateTime? LastUpdate { get; set; }

        public int? LastModifiedUser { get; set; }

        public FinanceOptionsData Options { get; set; }

        public int CapitalId { get; set; }

        public FinanceModel(Finance finance)
        {
            Id = finance.Id;
            DateFrom = finance.DateFrom;
            DateTo = finance.DateTo;
            LastUpdate = finance.LastUpdate;
            LastModifiedUser = finance.LastModifiedUser;
            CapitalId = finance.CapitalId;
            Options = JsonConvert.DeserializeObject<FinanceOptionsData>(finance.OptionsJson ?? "") ?? new();
        }
    }
}

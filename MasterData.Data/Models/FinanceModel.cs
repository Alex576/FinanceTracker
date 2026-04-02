using MasterData.Data.DBModels;
using MasterData.Data.Services;

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
        public FinanceOptionsDataExt? OptionsExt { get; set; }
        public FinanceType Type { get; set; }
        public int? Parent { get; set; }


        public FinanceModel(FinanceItem finance)
        {
            Id = finance.Id;
            DateFrom = finance.DateFrom;
            DateTo = finance.DateTo;
            LastUpdate = finance.LastUpdate;
            LastModifiedUser = finance.LastModifiedUser;
            Options = finance.OptionsJson ?? new();
            Type = (FinanceType)finance.FinanceType;
            Parent = finance.ParentFinanceId;
        }


        public async Task InitializeOptionsAsync(ObjectContextService objectContextService)
        {
            OptionsExt = new FinanceOptionsDataExt();
            OptionsExt.Objects.AddRange(await objectContextService.GetObjects(Options.ObjCodes));
            OptionsExt.Attributes = Options.Attributes.ToList();
            OptionsExt.ReadOnly = Options.ReadOnly;
        }
    }
}

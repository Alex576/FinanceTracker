using FinanceTracker.Data.DBModels;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceTracker.Data.Models
{
    public class LayoutModel<TData> where TData : class
    {
        public int TileCode { get; set; }
        public TData Layout { get; set; }

        public LayoutModel(Layout layout)
        {
            TileCode = layout.TileCode;
            Layout = JsonConvert.DeserializeObject<TData>(layout.LayoutJson ?? "") ?? Activator.CreateInstance<TData>();
        }
    }
}

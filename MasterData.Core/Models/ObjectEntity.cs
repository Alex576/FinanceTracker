using System;
using System.Collections.Generic;
using System.Text;

namespace MasterData.Core.Models
{
    public enum ObjectCode
    {

    }

    public class ObjectEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
    }
}

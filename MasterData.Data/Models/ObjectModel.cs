using MasterData.Data.DBModels;

namespace MasterData.Data.Models
{
    public class ObjectModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public int ClassCode { get; set; }

        public ObjectModel(ObjectEntity entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            FullName = entity.FullName;
            ClassCode = entity.ClassCode;
        }
    }
}

using MasterData.Data.DBContext;
using MasterData.Data.DBModels;
using MasterData.Data.Models;
using MasterData.Data.Services;
using Microsoft.EntityFrameworkCore;
using Shared.Observable;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterData.Data.Storages
{
    public class ObjectStorage : IMemoryStorage<ObjectModel>
    {
        private readonly ObjectContextService m_Context;
        private readonly string m_ObjectTableName;

        private readonly Dictionary<int, ObjectModel> m_Objects = [];
        private readonly Dictionary<int, List<ObjectModel>> m_ObjectsByClass = [];
        public ObjectStorage()
        {
            //m_ObjectTableName = m_Context.Context.GetTableName(typeof(ObjectEntity));
            //m_Context.TableChanged
            //    .Filter(x => x.Equals(m_ObjectTableName))
            //    .Subscribe(new AnonymousObserver<string>(_ => Load()));

            //Load();
        }


        public void Clear()
        {
            throw new NotImplementedException();
        }

        public ObjectModel Get(int id)
        {
            if (m_Objects.TryGetValue(id, out var obj))
                return obj;
            return null;
        }

        public async Task LoadAsync(MasterDataContext context)
        {
            var allObjects = await context.ObjectEntities.Select(x => new ObjectModel(x)).ToListAsync();
            //var allObjects = context.GetAll().GetAwaiter().GetResult();
            m_ObjectsByClass.Clear();
            foreach (var objsByClass in allObjects.GroupBy(x => x.ClassCode))
            {
                var objs = new List<ObjectModel>();
                foreach (var obj in objsByClass)
                {
                    m_Objects.Add(obj.Id, obj);
                    objs.Add(obj);
                }
                m_ObjectsByClass.Add(objsByClass.Key, objs);
            }
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Update(List<ObjectModel> tiles)
        {
            throw new NotImplementedException();
        }

        public List<ObjectModel> GetByClass(List<int> classCodes)
        {
            var objects = new List<ObjectModel>();
            foreach (var classCode in classCodes)
            {
                if (m_ObjectsByClass.TryGetValue(classCode, out var objs))
                    objects.AddRange(objs);
            }
            return objects;
        }

        public async Task SaveChangesAsync()
        {
            await m_Context.Context.SaveChangesAsync();
        }
    }
}

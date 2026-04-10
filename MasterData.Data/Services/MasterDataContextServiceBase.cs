using MasterData.Data.DBContext;
using Microsoft.EntityFrameworkCore;
using Shared.Observable;

namespace MasterData.Data.Services
{
    public class MasterDataContextServiceBase
    {
        protected readonly MasterDataContext m_Context;
        public MasterDataContext Context => m_Context;
        private readonly HashSet<string> m_TableChanges = [];
        private readonly Subject<string> m_TableChangedSub = new Subject<string>();
        public readonly IObservable<string> TableChanged;
        public MasterDataContextServiceBase(MasterDataContext context)
        {
            TableChanged = m_TableChangedSub.AsObservable();
            m_Context = context;
            m_Context.ChangeTracker.StateChanged += ChangeTracker_StateChanged;
            m_Context.SavedChanges += Context_SavedChanges;
        }

        private void Context_SavedChanges(object? sender, SavedChangesEventArgs e)
        {
            foreach (var change in m_TableChanges)
                m_TableChangedSub.OnNext(change);

            m_TableChanges.Clear();
        }

        private void ChangeTracker_StateChanged(object? sender, Microsoft.EntityFrameworkCore.ChangeTracking.EntityStateChangedEventArgs e)
        {
            if (e.NewState == EntityState.Deleted ||
                e.NewState == EntityState.Modified ||
                e.NewState == EntityState.Added)
            {
                var table = e.Entry.Metadata.GetTableName();
                if (!string.IsNullOrEmpty(table))
                    m_TableChanges.Add(table);
            }
        }
    }
}

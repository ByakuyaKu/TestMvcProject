using TestMvcProject.Data;
using TestMvcProject.Repository.Interfaces;

namespace TestMvcProject.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _appDbContext;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Add(T item) => _appDbContext.Add(item);

        public void Update(T item) => _appDbContext.Update(item);
        public void Remove(T item) => _appDbContext.Remove(item);
        public void Attach(T item) => _appDbContext.Attach(item);

        public void AddRange(List<T> items) => _appDbContext.AddRange(items);

        public void UpdateRange(List<T> items) => _appDbContext.UpdateRange(items);

        public void RemoveRange(List<T> items) => _appDbContext.RemoveRange(items);


        public async Task SaveChangesAsync() => await _appDbContext.SaveChangesAsync();

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _appDbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}

﻿namespace TestMvcProject.Repository.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        public void Add(T item);
        public void Update(T item);
        public void Remove(T item);

        public void AddRange(List<T> items);
        public void UpdateRange(List<T> items);
        public void RemoveRange(List<T> items);
        public void Attach(T item);
        public Task SaveChangesAsync();
    }
}

namespace XMorph.Currency.Repository.Generic.Interface {

    public interface IGenericRepository<T> where T : class {
        IQueryable<T> GetAll();
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
        void Save(T obj);
    }
}



namespace XMorph.Currency.Repository.Generic.Interface {
    using System.Collections.Generic;
    using System.Collections;

    public interface IGenericRepository<T> where T : class {
        IQueryable<T> GetAll();
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
    }
}

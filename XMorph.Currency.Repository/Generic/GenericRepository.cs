namespace XMorph.Currency.Repository.Generic {

    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using XMorph.Currency.DAL.DBContext;
    using XMorph.Currency.Repository.Generic.Interface;

    public class GenericRepository<T> : IGenericRepository<T> where T : class {

        private XMorphCurrencyContext _context = null;
        private DbSet<T> _table = null;

        public GenericRepository(XMorphCurrencyContext context) {
            _context = context;
            _table = context.Set<T>();
        }

        public GenericRepository() {
            _context = new XMorphCurrencyContext();
            _table = _context.Set<T>();
        }

        public virtual IQueryable<T> GetAll() {
            return _table;
        }

        public T GetById(object id) {
            return _table.Find(id);
        }
        public void Insert(T obj) {
            _table.Add(obj);
        }
        public void Update(T obj) {
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id) {
            T existing = _table.Find(id);
            _table.Remove(existing);
        }
        public void Save() {
            _context.SaveChanges();
        }
    }
}

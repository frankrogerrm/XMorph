using Microsoft.EntityFrameworkCore;
using XMorph.Currency.DAL.Entities;

namespace XMorph.Currency.DAL.DBContext {

    public class XMorphCurrencyContext : DbContext {
        public XMorphCurrencyContext(DbContextOptions<XMorphCurrencyContext> options) : base(options) {

        }
        public XMorphCurrencyContext() {

        }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanyFilter> CompanyFilters { get; set; }
        public virtual DbSet<CompanyFilterType> CompanyFilterTypes { get; set; }
        public virtual DbSet<CompanyRate> CompanyRates { get; set; }
        public virtual DbSet<User> Users { get; set; }

    }
}

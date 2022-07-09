namespace XMorph.Currency.Core.Services {

    using AgileObjects.AgileMapper;
    using XMorph.Currency.Core.Models;
    using XMorph.Currency.DAL.DBContext;

    public interface ICompanyFilterService {
        List<CompanyFilterModel> GetCompanyFilterByCompanyId(int companyId);
    }
    public class CompanyFilterService : ICompanyFilterService {

        private XMorphCurrencyContext _context;

        public CompanyFilterService(XMorphCurrencyContext context) {
            _context = context;
        }

        public List<CompanyFilterModel> GetCompanyFilterByCompanyId(int companyId) {

            var result = (from cf in _context.CompanyFilters
                          join cft in _context.CompanyFilterTypes on cf.FilterTypeId equals cft.Id
                          where cf.Companyid == companyId
                          select new CompanyFilterModel {
                              CompanyId = cf.Companyid,
                              FilterTypeId = cf.FilterTypeId,
                              Id = cf.Id,
                              KeyFilter = cf.KeyFilter,
                              CompanyFilterType = Mapper.Map(cft).ToANew<CompanyFilterTypeModel>()
                          }).ToList();

            return result;
        }
    }
}

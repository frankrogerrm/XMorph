namespace XMorph.Currency.Core.Services {

    using AgileObjects.AgileMapper;
    using XMorph.Currency.Core.Models;
    using XMorph.Currency.DAL.DBContext;
    using XMorph.Currency.DAL.Entities;
    using XMorph.Currency.Repository.Generic.Interface;

    public interface ICompanyFilterService {
        List<CompanyFilterModel> GetCompanyFilterByCompanyId(int companyId);
    }
    public class CompanyFilterService : ICompanyFilterService {

        private IGenericRepository<CompanyFilter> _companyFilter;
        private IGenericRepository<CompanyFilterType> _companyFilterType;

        public CompanyFilterService(IGenericRepository<CompanyFilter> companyFilter, 
                                    IGenericRepository<CompanyFilterType> companyFilterType) {
            _companyFilter = companyFilter;
            _companyFilterType = companyFilterType;
        }

        public List<CompanyFilterModel> GetCompanyFilterByCompanyId(int companyId) {

            var result = (from cf in _companyFilter.GetAll()
                          join cft in _companyFilterType.GetAll() on cf.FilterTypeId equals cft.Id
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

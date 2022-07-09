using System.Linq;

namespace XMorph.Currency.Core.Services {
    using AgileObjects.AgileMapper;
    using XMorph.Currency.Core.Models;
    using XMorph.Currency.DAL.DBContext;
    using XMorph.Currency.DAL.Entities;

    public interface ICompanyService {
        List<CompanyModel> GetAllActiveCompanies();
        CompanyModel GetCompanyByName(List<CompanyModel> companies, string companyName);
        CompanyModel GetCompanyById(int companyId);
    }

    public class CompanyService : ICompanyService
    {
        private XMorphCurrencyContext _context;
        private ICompanyFilterService _companyFilterService;

        public CompanyService(XMorphCurrencyContext context,
                                ICompanyFilterService companyFilterService) {
            _context = context;
            _companyFilterService = companyFilterService;
        }
        public List<CompanyModel> GetAllActiveCompanies() {
            var result = _context.Companies.Where(x => x.Status)
                .Select(x => Mapper.Map(x).ToANew<CompanyModel>())
                .ToList();
            result.ForEach(x => x.CompanyFilterModels = _companyFilterService.GetCompanyFilterByCompanyId(x.Id));
            return result;
        }

        public CompanyModel GetCompanyByName(List<CompanyModel> companies, string companyName) {
            if (companies == null || companies.Count == 0) {
                return new CompanyModel();
            }
            var result = companies.FirstOrDefault(x => x.Name.ToUpper().Equals(companyName.ToUpper()));

            if (result == null) {
                return new CompanyModel();
            }

            return result;
        }

        public CompanyModel GetCompanyById(int  companyid) {
            var result = _context.Companies.Where(x => x.Status && x.Id.Equals(companyid))
                .Select(x => Mapper.Map(x).ToANew<CompanyModel>())
                .FirstOrDefault();
            if (result == null) {
                return new CompanyModel();
            }
            result.CompanyFilterModels = _companyFilterService.GetCompanyFilterByCompanyId(result.Id);
            return result;
        }
    }
}

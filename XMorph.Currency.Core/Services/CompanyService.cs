using AgileObjects.AgileMapper;
using XMorph.Currency.Core.Models;
using XMorph.Currency.DAL.Entities;
using XMorph.Currency.Repository.Generic.Interface;

namespace XMorph.Currency.Core.Services {

    public interface ICompanyService {
        List<CompanyModel> GetAllActiveCompanies();
        CompanyModel GetCompanyByName(List<CompanyModel> companies, string companyName);
        CompanyModel GetCompanyById(int companyId);
    }

    public class CompanyService : ICompanyService {
        //private XMorphCurrencyContext _context;
        private ICompanyFilterService _companyFilterService;
        private IGenericRepository<Company> _companyRepository;

        public CompanyService(ICompanyFilterService companyFilterService,
                                IGenericRepository<Company> companyRepository) {

            _companyFilterService = companyFilterService;
            _companyRepository = companyRepository;
        }
        public List<CompanyModel> GetAllActiveCompanies() {
            var result = _companyRepository.GetAll().Where(x => x.Status)
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

        public CompanyModel GetCompanyById(int companyId) {
            var result = _companyRepository.GetAll().Where(x => x.Status && x.Id.Equals(companyId))
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

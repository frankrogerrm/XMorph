namespace XMorph.Currency.Core.Services {

    using AgileObjects.AgileMapper;
    using XMorph.Currency.Core.Models;
    using XMorph.Currency.DAL.DBContext;
    using XMorph.Currency.DAL.Entities;


    public interface ICompanyRateService {

        CompanyRateModel SaveUpdateCompanyRate(CompanyRateModel companyRateModel);
        List<CompanyRateModel> CleanCompanyRateByDays(int days);
        CompanyRateModel GetCompanyRateByCompanyId(int companyId);
        List<CompanyRateModel> GetAllCompanyRates();
        List<CompanyRateModel> ForceCleanCompanyRateByDays();

    }

    public class CompanyRateService : ICompanyRateService {

        private XMorphCurrencyContext _context;

        public CompanyRateService(XMorphCurrencyContext context) {

            _context = context;

        }

        public CompanyRateModel SaveUpdateCompanyRate(CompanyRateModel companyRateModel) {

            var model = Mapper.Map(companyRateModel).ToANew<CompanyRate>();
            var companiesModel = _context.CompanyRates
                .Where(x => x.CompanyId.Equals(model.CompanyId) && x.Status)
                .ToList();
            companiesModel.ForEach(x => {
                x.Status = false;
                x.UpdatedDate = DateTime.Now;
            });

            _context.CompanyRates.Add(model);
            _context.SaveChanges();
            return Mapper.Map(model).ToANew<CompanyRateModel>();

        }

        public List<CompanyRateModel> CleanCompanyRateByDays(int days) {

            var datetimeToRemove = DateTime.Now.AddDays(-1 * days);
            var ratesToRemove = _context.CompanyRates
                .Where(x => !x.Status && x.UpdatedDate < datetimeToRemove)
                .ToList();
            _context.CompanyRates.RemoveRange(ratesToRemove);
            _context.SaveChanges();

            return ratesToRemove.Select(x => Mapper.Map(x).ToANew<CompanyRateModel>()).ToList();

        }

        public List<CompanyRateModel> ForceCleanCompanyRateByDays() {
            
            var ratesToRemove = _context.CompanyRates
                .Where(x => !x.Status)
                .ToList();
            _context.CompanyRates.RemoveRange(ratesToRemove);
            _context.SaveChanges();

            return ratesToRemove.Select(x => Mapper.Map(x).ToANew<CompanyRateModel>()).ToList();

        }

        public CompanyRateModel GetCompanyRateByCompanyId(int companyId) {

            var result = _context.CompanyRates.FirstOrDefault(x => x.Status && x.CompanyId.Equals(companyId));
            return Mapper.Map(result).ToANew<CompanyRateModel>();

        }

        public List<CompanyRateModel> GetAllCompanyRates() {

            var result = _context.CompanyRates.Where(x => x.Status)
                .Select(x => Mapper.Map(x).ToANew<CompanyRateModel>()).ToList();
            return result;

        }

    }
}

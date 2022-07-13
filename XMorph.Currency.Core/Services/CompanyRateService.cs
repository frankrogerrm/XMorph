using AgileObjects.AgileMapper.Extensions;
using Microsoft.EntityFrameworkCore.Design;
using XMorph.Currency.Repository.Generic.Interface;

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

        //private XMorphCurrencyContext _context;
        private IGenericRepository<CompanyRate> _companyRateRepository;
        private IGenericRepository<Company> _companyRepository;

        public CompanyRateService(IGenericRepository<CompanyRate> companyRateRepository, IGenericRepository<Company> companyRepository) {

            _companyRateRepository = companyRateRepository;
            _companyRepository = companyRepository;

        }

        public CompanyRateModel SaveUpdateCompanyRate(CompanyRateModel companyRateModel) {

            var model = Mapper.Map(companyRateModel).ToANew<CompanyRate>();
            var companiesModel = _companyRateRepository.GetAll()
                .Where(x => x.CompanyId.Equals(model.CompanyId) && x.Status)
                .ToList();
            companiesModel.ForEach(x => {
                x.Status = false;
                x.UpdatedDate = DateTime.Now;
            });

            _companyRateRepository.Insert(model);
            _companyRateRepository.Save();
            return Mapper.Map(model).ToANew<CompanyRateModel>();

        }

        public List<CompanyRateModel> CleanCompanyRateByDays(int days) {

            var datetimeToRemove = DateTime.Now.AddDays(-1 * days);
            var ratesToRemove = _companyRateRepository.GetAll()
                .Where(x => !x.Status && x.UpdatedDate < datetimeToRemove)
                .ToList();
            _companyRateRepository.Delete(ratesToRemove);
            _companyRateRepository.Save();

            return ratesToRemove.Select(x => Mapper.Map(x).ToANew<CompanyRateModel>()).ToList();

        }

        public List<CompanyRateModel> ForceCleanCompanyRateByDays() {

            var ratesToRemove = _companyRateRepository.GetAll()
                .Where(x => !x.Status)
                .ToList();
            _companyRateRepository.Delete(ratesToRemove);
            _companyRateRepository.Save();

            return ratesToRemove.Select(x => Mapper.Map(x).ToANew<CompanyRateModel>()).ToList();

        }

        public CompanyRateModel GetCompanyRateByCompanyId(int companyId) {
            var result = (_companyRateRepository.GetAll()
                .Join(_companyRepository.GetAll(), cr => cr.CompanyId, c => c.Id, (cr, c) => new { cr, c })
                .Where(t => t.c.Status && t.cr.Status && t.c.Id.Equals(companyId))
                .Select(t => MapToCompanyRateModel(t.cr, t.c))).FirstOrDefault();

            return result!;

        }

        private static CompanyRateModel MapToCompanyRateModel(CompanyRate companyRate, Company company) {
            var model = Mapper.Map(companyRate).ToANew<CompanyRateModel>();
            model.CompanyName = company.Name;
            return model;
        }

        public List<CompanyRateModel> GetAllCompanyRates() {

            var result = (_companyRateRepository.GetAll()
                .Join(_companyRepository.GetAll(), cr => cr.CompanyId, c => c.Id, (cr, c) => new { cr, c })
                .Where(t => t.cr.Status && t.c.Status)
                .Select(t => MapToCompanyRateModel(t.cr, t.c))).ToList();

            return result;

        }

    }
}

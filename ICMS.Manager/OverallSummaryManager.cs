using ICMS.Data.Infrastructure;
using ICMS.Data.Repositories;
using ICMS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Manager
{
    // operations you want to expose
    public interface IOverallSummaryManager
    {
        IEnumerable<OverallSummary> GetOverallSummary(string studentId, string typeCode);
    }
    class OverallSummaryManager : IOverallSummaryManager
    {
        private readonly IOverallSummaryRepository overallSummaryRepository;
        private readonly IUnitOfWork unitOfWork;

        public OverallSummaryManager(IOverallSummaryRepository overallSummaryRepository, IUnitOfWork unitOfWork)
        {
            this.overallSummaryRepository = overallSummaryRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<OverallSummary> GetOverallSummary(string studentId, string typeCode)
        {
            if (string.IsNullOrEmpty(typeCode))
                return null;
            else
                return overallSummaryRepository.GetOverallSummary(studentId, typeCode.ToUpper());
        }
    }
}

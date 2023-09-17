using AP_ShopBE.BLL.Interface;
using AP_ShopBE.DAL.Interface;
using AP_ShopBE.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.BLL.Services
{
    public class ConditionService : IConditionService
    {
        private IConditionRepository conditionRepository;
        public ConditionService(IConditionRepository conditionRepository)
        {
            this.conditionRepository = conditionRepository;
        }
        public Task<List<Condition>> GetConditions()
        {
            return conditionRepository.GetConditions();
        }
        public Task<Condition> GetCondition(int id)
        {
            return conditionRepository.GetCondition(id);
        }
    }
}

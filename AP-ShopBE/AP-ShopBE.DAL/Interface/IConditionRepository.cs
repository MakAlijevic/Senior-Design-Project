using AP_ShopBE.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.DAL.Interface
{
    public interface IConditionRepository
    {
        Task<List<Condition>> GetConditions();
        Task<Condition> GetCondition(int id);
    }
}

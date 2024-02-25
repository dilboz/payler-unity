using System.Collections.Generic;
using System.Threading.Tasks;

namespace Script.Server.Routs.Tariff
{
    public interface ITariff
    {
        public Task<List<Model.Tariff.Tariff>> GetTariffs();
    }
}
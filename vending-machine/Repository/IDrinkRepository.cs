using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vending_machine.Models;

namespace vending_machine.Repository
{
    public interface IDrinkRepository : IRepository<Drink>
    {
        Task<IEnumerable<Drink>> GetAll();
        Task<IEnumerable<Drink>> GetDrinkByPrice();
        Task<Drink> GetDrink(string drinkName, float money);
        Task<IEnumerable<Drink>> GetAmountByDrink(int id);
    }
}

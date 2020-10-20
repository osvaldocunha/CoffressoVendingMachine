using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vending_machine.Repository
{
    public interface IUnityOfWork
    {
        IDrinkRepository  DrinkRepository { get; }
      
        Task Commit();
    }
}

using StringReverseService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StringReverseService.Repository
{
   public interface IInputStringRepository
    {
        IEnumerable<InputString> ListAllInputs();

        void InsertInputString(InputString input);

        InputString GetByID(Guid id); 

        void Save();
    }
}

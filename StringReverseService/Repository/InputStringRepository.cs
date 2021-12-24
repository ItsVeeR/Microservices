using StringReverseService.DBContext;
using StringReverseService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StringReverseService.Repository
{
    public class InputStringRepository : IInputStringRepository
    {
        private readonly InputStringContext dbContext;

        public InputStringRepository(InputStringContext dbContext)
        {
           this.dbContext = dbContext;
        }

        public void InsertInputString(InputString input)
        {
            this.dbContext.Add(input);
            Save();
        }

        public IEnumerable<InputString> ListAllInputs()
        {
            return this.dbContext.InputStrings.ToList();
        }

        public InputString GetByID(Guid id)
        {
            return this.dbContext.InputStrings.Find(id);
        }

        

        public void Save()
        {
            this.dbContext.SaveChanges();
        }
    }
}

using BrixBank.Data.Entities;
using System.Collections.Generic;

namespace BrixBank.webApi.DTO
{
    public class LoanRequestDTO
    {
        public int Id { get; set; }
        public string LoanRequestrId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Dictionary<string, int> DictionaryData { get; set; }
        public int LoanSupplied { get; set; }

    }

}

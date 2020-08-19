using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BrixBank.Data.Entities
{
   public class LoanRequest
    {
       
        public int Id { get; set; }
        public string LoanRequestrId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public Dictionary<string,int> DictionaryData { get; set; }
        public int LoanSupplied { get; set; }

    }
}

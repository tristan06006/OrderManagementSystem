using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.Models
{
    public class Transactions
    {
        public string operationType { get; set; }
        public string operationOnTable { get; set; }
        public string operationOnTableId { get; set; }
        public string operationOnTableRevId { get; set; }
        public List<TransactionItem> TransactionItem { get; set; }
    }

    public class TransactionItem
    {
        public string key { get; set; }
        public string value { get; set; }
    }
}
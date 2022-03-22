using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    class AuditEntry
    {
        public string TransactionType { get; set; }
        public decimal TransactionAmount { get; set; }
        public decimal Balance { get; set; }
        public string TimeStamp { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }

        public AuditEntry(string transactionType, decimal transactionAmount, decimal balance)
        {
            TransactionType = transactionType;
            TransactionAmount = transactionAmount;
            Balance = balance;
            TimeStamp = DateTime.Now.ToString();
        }

        public AuditEntry(int quantity, CateringItem cateringItem, decimal transactionAmount, decimal balance)
        {
            Quantity = quantity;
            TransactionAmount = transactionAmount;
            Balance = balance;
            Description = cateringItem.Description;
            Code = cateringItem.Code;
            TimeStamp = DateTime.Now.ToString();
        }

    }
}

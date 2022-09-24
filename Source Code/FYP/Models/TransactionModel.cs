using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FYP.Models
{
    public class TransactionModel
    {
        public string Id { get; set; }
        public string Amount { get; set; }
        public bool Status { get; set; }
        public string Card_Number { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public int Customer_Id { get; set; }
    }
}
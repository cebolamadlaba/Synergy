using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Model.UserInterface.Bol
{
    public class BolFinancial
    {
        public int Id { get; set; }

        public int RiskGroupId { get; set; }

        public decimal TotalPayments { get; set; }

        public decimal TotalCollections { get; set; }

        public decimal TotalValueAdded { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.DTO.ReferenceData
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Description { get; set; }

        public ConditionType ConditionType { get; set; }
    }
}

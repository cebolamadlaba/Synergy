using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.DTO.Enums
{

    public enum ConcessionTypes : int
    {

        [EnumMember(Value = "NotSet")]
        NotSet = 0,


        [EnumMember(Value = "Lending")]
        Lending = 1,

        [EnumMember(Value = "Investment")]
        Investment = 2,


        [EnumMember(Value = "Mas")]
        Mas = 3,

        [EnumMember(Value = "Trade")]
        Trade = 4,
        
       
        [EnumMember(Value = "Transactional")]
        Transactional = 5,


        [EnumMember(Value = "Bol")]
        Bol = 6,


        [EnumMember(Value = "Cash")]
        Cash = 7,

    }
}

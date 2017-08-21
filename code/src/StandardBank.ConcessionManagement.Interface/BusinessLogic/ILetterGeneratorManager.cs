using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    public interface ILetterGeneratorManager
    {
        byte[] GenerateLetters(IEnumerable<int> concessionIds);
    }
}

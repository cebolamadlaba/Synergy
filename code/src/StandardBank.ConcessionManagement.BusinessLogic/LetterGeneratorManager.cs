using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    public class LetterGeneratorManager : ILetterGeneratorManager
    {
        public LetterGeneratorManager()
        {

        }
        public byte[] GenerateLetters(IEnumerable<int> concessionIds)
        {
            return new byte[0];
            //throw new NotImplementedException();
        }
    }
}

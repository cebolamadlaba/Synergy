using System;
using System.Collections.Generic;
using System.Text;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Common
{
    public class XmlMarshaller : IMarshaller
    {
        public string SerializeObject<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public T DeserializeObject<T>(string entity)
        {
            throw new NotImplementedException();
        }
    }
}

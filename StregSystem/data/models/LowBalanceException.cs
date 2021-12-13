using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem.data.models
{
    [Serializable]
    public class LowBalanceException : Exception
    {
        public LowBalanceException() { }
        public LowBalanceException(string message)
            : base(message)
        { }
        public LowBalanceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

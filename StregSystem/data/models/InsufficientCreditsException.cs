using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem.data.models
{   
    [Serializable]
    class InsufficientCreditsException : Exception
    {
        public InsufficientCreditsException() { }
        public InsufficientCreditsException(string message)
            :base(message)
        { }
        public InsufficientCreditsException(string message, Exception innerException)
            :base(message, innerException)
        { }
    }
}

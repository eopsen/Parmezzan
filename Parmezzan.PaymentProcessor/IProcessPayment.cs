using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parmezzan.PaymentProcessor
{
    public  interface IProcessPayment
    {
        bool PaymentProcess();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorCore
{
    [ServiceContract(Namespace = "http://localhost/calculatorcode")]
    public interface ICalculator
    {
        [OperationContract]
        int Add(int x, int y);

        [OperationContract]
        int Subtract(int x, int y);
    }
}

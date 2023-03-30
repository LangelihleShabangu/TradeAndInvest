using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ClientManagement.Console.ProcessingService.Interface
{    
    [ServiceContract]
    public interface IProcessingService
    {
        [OperationContract]
        void SendAccountMessage(string request, string PaymentSource);        
    }   
}

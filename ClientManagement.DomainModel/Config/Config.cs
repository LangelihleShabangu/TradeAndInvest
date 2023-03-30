using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ClientManagement.DomainModel.Config
{
    public class Config
    {
        public static TransactionOptions DefaultTransactionOptions
        {
            get
            {
                return new TransactionOptions()
                {
                    IsolationLevel = IsolationLevel.ReadCommitted
                };
            }

        }
    }
}

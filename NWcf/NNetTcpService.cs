using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace NWcf
{
    public class NNetTcpService<TContract, TService>
    {
        private Type serviceType;
        private Uri baseAddress;
        private string endPointAddress;
        private ServiceHost serviceHost;

        public NNetTcpService(string host, int port)
            : this("net.tcp://" + host + ":" + port + "/nwcf", typeof(TContract).FullName)
        {
        }

        public NNetTcpService(string baseAddress, string endPointAddress)
            : this(new Uri(baseAddress), endPointAddress)
        {
        }

        public NNetTcpService(Uri baseAddress, string endPointAddress)
        {
            this.serviceType = typeof(TService);
            this.baseAddress = baseAddress;
            this.endPointAddress = endPointAddress;
        }

        public void Start()
        {
            serviceHost = new ServiceHost(typeof(TService), baseAddress);
            serviceHost.AddServiceEndpoint(typeof(TContract), new NetTcpBinding(), endPointAddress);
            serviceHost.Open();
        }

        public void Stop()
        {
            serviceHost.Close();
        }
    }
}

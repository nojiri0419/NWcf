using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace NWcf
{
    public class NNetTcpClient<TContract>
    {
        private string endPointAddress;
        private ChannelFactory<TContract> channelFactory;

        public NNetTcpClient(string host, int port)
            : this("net.tcp://" + host + ":" + port + "/nwcf/" + typeof(TContract).FullName)
        {
        }

        public NNetTcpClient(string endPointAddress)
        {
            this.endPointAddress = endPointAddress;
        }

        public void Open()
        {
            channelFactory = new ChannelFactory<TContract>(new NetTcpBinding());
        }

        public void Close()
        {
            channelFactory.Close();
            channelFactory = null;
        }
        public TContract GetClient()
        {
            return channelFactory.CreateChannel(new EndpointAddress(endPointAddress));
        }
    }
}

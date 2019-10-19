using System;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Factory
{
    public sealed class WCFFactory<TChannel> : ChannelFactory<TChannel>
    {
        public static WCFFactory<TChannel> GetFactorty()
        {
            WSHttpBinding binding = new WSHttpBinding();
            EndpointAddress address = new EndpointAddress(new Uri(string.Format("http://{0}/Bll/",ConfigurationManager.AppSettings["hostIP"])));
            binding.MaxReceivedMessageSize = 6553600;
            binding.MaxBufferPoolSize = 52428800;
            binding.ReaderQuotas.MaxDepth = 3200;
            binding.ReaderQuotas.MaxStringContentLength = 819200;
            binding.ReaderQuotas.MaxArrayLength = 16384;
            binding.ReaderQuotas.MaxBytesPerRead = 409600;
            binding.ReaderQuotas.MaxNameTableCharCount = 16384;
            binding.ReceiveTimeout = new TimeSpan(0,5,0);        

            ContractDescription description = ContractDescription.GetContract(typeof(TChannel));

            ServiceEndpoint endpoint = new ServiceEndpoint(description, binding, address);
            return new WCFFactory<TChannel>(endpoint);
        }

        private WCFFactory(ServiceEndpoint endpoint)
            : base(endpoint)
        {

        }
    }
}

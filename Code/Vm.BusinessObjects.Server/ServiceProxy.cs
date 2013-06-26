using System;
using System.ServiceModel;

namespace Vm.BusinessObjects.Server
{
    public class ServiceProxy<TContract> : IDisposable
        where TContract : class
    {
        #region Private fields

        private TContract _channel;
        private bool _disposed = false;
        private readonly object _sync = new object();
        private readonly string _serviceEndpointAddress;
        private ChannelFactory<TContract> _channelFactory;

        #endregion

        public ServiceProxy(string serviceEndpointAddress)
        {
            _serviceEndpointAddress = serviceEndpointAddress;
        }

        // For the moment we do not need Disposable pattern with Finalizer since we do not have unmanaged resources.
        //~ServiceProxy()
        //{
        //    Dispose(false);
        //}

        public TContract GetChannel()
        {
            InitializeManagedResources();

            return _channel;
        }

        public void Dispose()
        {
            Dispose(true);
            //GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    DisposeManagedResources();

            _disposed = true;
        }

        #region Private methods

        private void InitializeManagedResources()
        {
            if (_disposed)
                throw new ObjectDisposedException("Current instance was already disposed.");

            lock (_sync)
            {
                if (_channel == null)
                {
                    _channelFactory = new ChannelFactory<TContract>(new NetTcpBinding());
                    _channel = _channelFactory.CreateChannel(new EndpointAddress(_serviceEndpointAddress));
                }
            }
        }

        private void DisposeManagedResources()
        {
            lock (_sync)
            {
                if (_channel != null)
                {
                    ((ICommunicationObject) _channel).Close();
                    _channel = null;
                }

                if (_channelFactory != null)
                {
                    ((IDisposable) _channelFactory).Dispose();
                    _channelFactory = null;
                }
            }
        }

        #endregion
    }
}
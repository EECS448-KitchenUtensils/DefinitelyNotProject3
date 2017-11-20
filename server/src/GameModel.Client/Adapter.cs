using GameModel.Messages;
using System.Collections.Concurrent;

namespace GameModel.Client
{
    /// <summary>
    /// Base class for Adapters
    /// Used to communicate with e.g. a local client or remote server
    /// </summary>
    public abstract class Adapter
    {
        protected Adapter()
        {
            _queue = new ConcurrentQueue<ModelMessage>();
        }

        protected ConcurrentQueue<ModelMessage> _queue;
    }
}

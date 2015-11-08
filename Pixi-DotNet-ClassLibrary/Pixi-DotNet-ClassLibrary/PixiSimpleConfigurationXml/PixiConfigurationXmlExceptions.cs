using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixi.Configuration
{
    [Serializable()]
    public class PixiConfigFileAllreadyExistException : System.Exception
    {
        public PixiConfigFileAllreadyExistException() : base() { }
        public PixiConfigFileAllreadyExistException(string message) : base(message) { }
        public PixiConfigFileAllreadyExistException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected PixiConfigFileAllreadyExistException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        { }
    }

    [Serializable()]
    public class PixiConfigFileNotExistException : System.Exception
    {
        public PixiConfigFileNotExistException() : base() { }
        public PixiConfigFileNotExistException(string message) : base(message) { }
        public PixiConfigFileNotExistException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected PixiConfigFileNotExistException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        { }
    }

}

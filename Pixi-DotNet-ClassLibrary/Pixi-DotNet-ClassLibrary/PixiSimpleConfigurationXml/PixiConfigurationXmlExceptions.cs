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
    }

    [Serializable()]
    public class PixiConfigFileNotExistException : System.Exception
    {
        public PixiConfigFileNotExistException() : base() { }
        public PixiConfigFileNotExistException(string message) : base(message) { }
        public PixiConfigFileNotExistException(string message, System.Exception inner) : base(message, inner) { }
    }

}

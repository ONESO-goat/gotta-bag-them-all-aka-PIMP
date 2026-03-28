using System;


// class ExceptionTypes;
static public class movementException : System.Exception
        {
            public movementException() { }
            public movementException(string message) : base(message) { }
            public movementException(string message, System.Exception inner) : base(message, inner) { }
            protected movementException(
                System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }
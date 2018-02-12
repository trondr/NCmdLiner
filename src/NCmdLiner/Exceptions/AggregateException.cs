#if NET35
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NCmdLiner.Exceptions
{
    public class AggregateException : Exception
    {
        private readonly ReadOnlyCollection<Exception> _innerExceptions;

        private AggregateException() { }

        public AggregateException(IList<Exception> exceptionArray)
        {
            _innerExceptions = new ReadOnlyCollection<Exception>(exceptionArray);
        }

        public ReadOnlyCollection<Exception> InnerExceptions
        {
            get
            {
                return this._innerExceptions;
            }
        }
    }
}
#endif

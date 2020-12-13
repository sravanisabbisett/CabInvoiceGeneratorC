using System;
using System.Collections.Generic;
using System.Text;

namespace CabInvoiceGeneerator
{
    public class InvoiceException:Exception
    {
        //Enum constants for different type of exceptions
        public enum ExceptionType
        {
            NULL_RIDES, INVALID_USER_ID, INVALID_DISTANCE, INVALID_TIME, INVALID_RIDE_TYPE
        }

        public ExceptionType exception;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public InvoiceException(ExceptionType exception,string message):base(message)
        {
            this.exception = exception;
        }
         
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SatSolver.Utilities
{
    public class InvalidNetListFileException : Exception
    {

        public string FilePath { get; private set; }
        public int LineNumber { get; private set; }
        
        public InvalidNetListFileException()
        {
        }

        public InvalidNetListFileException(string message) : base(message)
        {
        }

        /// <summary>
        /// Construct an <see cref="InvalidNetListFileException"/>
        /// </summary>
        /// <param name="message">Message about the exception</param>
        /// <param name="filePath">Path to the file which generated this exception</param>
        /// <param name="lineNumber">Line of the file which the exception occured in</param>
        public InvalidNetListFileException(string message,
            string filePath, int lineNumber) : base(message)
        {
            FilePath = filePath;
            LineNumber = lineNumber;
        }

        public InvalidNetListFileException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidNetListFileException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Error in file " + Path.GetFileName(FilePath));
            sb.AppendLine(Message);
            sb.AppendLine("\tIn line #" + LineNumber);

            return sb.ToString();
        }
    }
}

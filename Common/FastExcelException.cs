using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class FastExcelException : Exception
    {
        public FastExcelException() { }

        public FastExcelException(string message) : base(message) { }


    }

    public class FastExcelFormatException : FastExcelException
    {

        public FastExcelFormatException() { }

        public FastExcelFormatException(string message) : base(message) { }

    }
}

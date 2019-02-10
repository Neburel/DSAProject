using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.util.ErrrorManagment
{
    public class Error
    {
        public ErrorCode ErrorCode { get; set; } = ErrorCode.Error;
        public string Message { get; set; } = string.Empty;
    }
}

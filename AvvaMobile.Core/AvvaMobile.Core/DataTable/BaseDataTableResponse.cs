using AvvaMobile.Core.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvvaMobile.Core.DataTable
{
    public class BaseDataTableResponse<T> : ServiceResult
    {
        public int RecordsFiltered { get; set; }
        public int RecordsTotal { get; set; }
        public T Data { get; set; }
    }
}
using AvvaMobile.Core.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvvaMobile.Core.DataTable
{
    public class ApiBaseDatatableResponse<T>:ServiceResult
    {
        public T Data { get; set; }
        public Meta Meta { get; set; }
    }
    public class Meta
    {
        public int page { get; set; }
        public int pages { get; set; }
        public int perpage { get; set; }
        public int total { get; set; }
        public string sortdir { get; set; }
        public string sortcol { get; set; }
    }
}

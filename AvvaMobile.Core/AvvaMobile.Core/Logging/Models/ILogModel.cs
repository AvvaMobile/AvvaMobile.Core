using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvvaMobile.Core.Logging.Models;

public interface ILogModel
{
    public int ID { get; set; }

    public string Type { get; set; }

    public DateTime CreatedDate { get; set; }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvvaMobile.Core.Services.Models
{
    [Table("Common.AppSettings")]
    public class AppSettings
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public string Key { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public string Placeholder { get; set; }
        public int Order { get; set; }
    }
}

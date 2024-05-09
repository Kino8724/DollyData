using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollyData.Models
{
    public class DbObject
    {
        /// <summary>
        /// Gets or sets the database id.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}

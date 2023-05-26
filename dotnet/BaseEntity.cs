using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hasty.Models.Requests.Ratings
{
    public class BaseEntity
    {
        [Required, Range(1, int.MaxValue)]
        public int EntityTypeId { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int EntityId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hasty.Models.Domain.Ratings
{
    public class Rating
    {
        public int Id { get; set; }

        public int RatingVal { get; set; }

        public LookUp Comment { get; set; }

        public LookUp EntityType { get; set; }

        public int EntityId { get; set; }

        public BaseUser CreatedBy { get; set; }

        public DateTime DateCreated { get; set; }

        public BaseUser ModifiedBy { get; set; }

        public DateTime DateModified { get; set; }

        public bool isDeleted { get; set; }
    }
}

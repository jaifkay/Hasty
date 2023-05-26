using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hasty.Models.Requests.Ratings
{
    public class RatingAddRequest: BaseEntity
    {
        [Required, Range(1,5,ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public byte RatingVal { get; set; }

        [Range(1, int.MaxValue)]
        public int? CommentId { get; set; }
    }
}

using Hasty.Models;
using Hasty.Models.Domain.Ratings;
using Hasty.Models.Requests.Ratings;

namespace Hasty.Services
{
    public interface IRatingService
    {
        Paged<Rating> GetByEntityId(int pageIndex, int pageSize, BaseEntity model);

        RatingSummary GetRatingSummary(int entityId, int entityTypeId);

        public int Create(RatingAddRequest model, int userId);

        public void Update(RatingUpdateRequest model, int userId);

        public void Delete(int id, int userId);
    }
}
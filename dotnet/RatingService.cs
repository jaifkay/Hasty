using Hasty.Data;
using Hasty.Data.Providers;
using Hasty.Models;
using Hasty.Models.Domain;
using Hasty.Models.Domain.Ratings;
using Hasty.Models.Requests.Ratings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hasty.Services
{
    public class RatingService : IRatingService, IRatingService
    {
        IDataProvider _data = null;

        public RatingService(IDataProvider data)
        {
            _data = data;
        }

        public Paged<Rating> GetByEntityId(int pageIndex, int pageSize, BaseEntity model)
        {
            string procName = "[dbo].[Ratings_Select_ByEntityId]";

            Paged<Rating> pagination = null;

            List<Rating> ratings = null;

            int totalCount = 0;

            _data.ExecuteCmd(procName, inputParamMapper: delegate (SqlParameterCollection collection)
            {
                collection.AddWithValue("@PageIndex", pageIndex);
                collection.AddWithValue("@PageSize", pageSize);
                collection.AddWithValue("@EntityId", model.EntityId);
                collection.AddWithValue("@EntityTypeId", model.EntityTypeId);

            }, singleRecordMapper: delegate (IDataReader reader, short set)
            {
                int paramIndex = 0;

                Rating rating = SingleRatingMapper(reader, ref paramIndex);

                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(paramIndex++);
                }

                if (ratings == null)
                {
                    ratings = new List<Rating>();
                }
                ratings.Add(rating);
            });
            if (ratings != null)
            {
                pagination = new Paged<Rating>(ratings, pageIndex, pageSize, totalCount);
            }
            return pagination;
        }

        public RatingSummary GetRatingSummary(int entityId, int entityTypeId)
        {

            string procName = "[dbo].[Ratings_SelectSummary_ByEntityId]";
            RatingSummary ratingSummary = null;

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection collection)
            {
                collection.AddWithValue("@EntityId", entityId);
                collection.AddWithValue("@EntityTypeId", entityTypeId);

            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                ratingSummary = new RatingSummary();
                ratingSummary.Average = reader.GetSafeDecimal(startingIndex++);
                ratingSummary.TotalCount = reader.GetSafeInt32(startingIndex++);

            });
            return ratingSummary;
        }

        public int Create(RatingAddRequest model, int userId)
        {
            string procName = "[dbo].[Ratings_Insert]";

            int id = 0;

            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection collection)
            {
                AddCommonParams(model, userId, collection);

                SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                idOut.Direction = ParameterDirection.Output;
                collection.Add(idOut);

            }, returnParameters: delegate (SqlParameterCollection returnCollection)
            {
                object idOut = returnCollection["@Id"].Value;
                int.TryParse(idOut.ToString(), out id);
            });
            return id;
        }

        public void Update(RatingUpdateRequest model, int userId)
        {
            string procName = "[dbo].[Ratings_Update]";

            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection collection)
            {
                collection.AddWithValue("@Id", model.Id);
                collection.AddWithValue("@Rating", model.RatingVal);
                collection.AddWithValue("@ModifiedBy", userId);

            }, returnParameters: null);
        }

        public void Delete(int id, int userId)
        {
            string procName = "[dbo].[Ratings_Delete_ById]";

            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection collection)
            {
                collection.AddWithValue("@Id", id);
                collection.AddWithValue("@ModifiedBy", userId);

            }, returnParameters: null);
        }

        private static void AddCommonParams(RatingAddRequest model, int userId, SqlParameterCollection collection)
        {
            collection.AddWithValue("@Rating", model.RatingVal);
            collection.AddWithValue("@CommentId", model.CommentId != null ? model.CommentId : DBNull.Value);
            collection.AddWithValue("@EntityTypeId", model.EntityTypeId);
            collection.AddWithValue("@EntityId", model.EntityId);
            collection.AddWithValue("@CreatedBy", userId);
        }

        private static Rating SingleRatingMapper(IDataReader reader, ref int paramIndex)
        {
            Rating rating = new Rating
            {
                Comment = new LookUp(),
                EntityType = new LookUp(),
                CreatedBy = new BaseUser(),
                ModifiedBy = new BaseUser()
            };
            rating.Id = reader.GetInt32(paramIndex++);
            rating.RatingVal = reader.GetByte(paramIndex++);
            rating.Comment.Id = reader.GetSafeInt32(paramIndex++);
            rating.Comment.Name = reader.GetSafeString(paramIndex++);
            rating.EntityType.Id = reader.GetInt32(paramIndex++);
            rating.EntityType.Name = reader.GetString(paramIndex++);
            rating.EntityId = reader.GetInt32(paramIndex++);
            rating.CreatedBy.Id = reader.GetInt32(paramIndex++);
            rating.CreatedBy.FirstName = reader.GetString(paramIndex++);
            rating.CreatedBy.LastName = reader.GetString(paramIndex++);
            rating.CreatedBy.Mi = reader.GetString(paramIndex++);
            rating.CreatedBy.AvatarUrl = reader.GetString(paramIndex++);
            rating.DateCreated = reader.GetDateTime(paramIndex++);
            rating.ModifiedBy.Id = reader.GetInt32(paramIndex++);
            rating.ModifiedBy.FirstName = reader.GetString(paramIndex++);
            rating.ModifiedBy.LastName = reader.GetString(paramIndex++);
            rating.ModifiedBy.Mi = reader.GetString(paramIndex++);
            rating.ModifiedBy.AvatarUrl = reader.GetString(paramIndex++);
            rating.DateModified = reader.GetDateTime(paramIndex++);
            rating.isDeleted = reader.GetBoolean(paramIndex++);
            return rating;
        }
    }
}

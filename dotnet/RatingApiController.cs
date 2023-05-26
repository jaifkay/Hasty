using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Hasty.Models;
using Hasty.Models.Domain.Ratings;
using Hasty.Models.Requests.Ratings;
using Hasty.Services;
using Hasty.Web.Controllers;
using Hasty.Web.Models.Responses;
using System;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace Hasty.Web.Api.Controllers
{
    [Route("api/ratings")]
    [ApiController]
    public class RatingApiController : BaseApiController
    {
        IRatingService _service = null;
        IAuthenticationService<int> _authService = null;

        public RatingApiController(
            IRatingService service,
            ILogger<RatingApiController> logger,
            IAuthenticationService<int> auth) : base(logger)
        {
            _service = service;
            _authService = auth;
        }

        [HttpGet("paginate")]
        public ActionResult<ItemResponse<Paged<Rating>>> GetByEntityId(int pageIndex, int pageSize, BaseEntity model)
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                Paged<Rating> page = _service.GetByEntityId(pageIndex, pageSize, model);

                if (page == null)
                {
                    code = 404;
                    response = new ErrorResponse("No Ratings found.");
                }
                else
                {
                    response = new ItemResponse<Paged<Rating>> { Item = page };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
                base.Logger.LogError(ex.ToString());
            }
            return StatusCode(code, response);
        }

        [HttpGet("summary")]
        public ActionResult<ItemResponse<RatingSummary>> Get(int entityId, int entityTypeId)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                RatingSummary ratingSummary = _service.GetRatingSummary(entityId, entityTypeId);

                if (ratingSummary == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemResponse<RatingSummary> { Item = ratingSummary };
                }
            }
            catch (Exception ex)
            {

                iCode = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Error: {ex.Message}");
            }

            return StatusCode(iCode, response);

        }

        [HttpPost]
        public ActionResult<ItemResponse<int>> Add(RatingAddRequest model)
        {
            ObjectResult result = null;

            try
            {
                int userId = _authService.GetCurrentUserId();

                int id = _service.Create(model, userId);
                ItemResponse<int> response = new ItemResponse<int>() { Item = id };

                result = Created201(response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                ErrorResponse response = new ErrorResponse(ex.Message);
                result = StatusCode(500, response);
            }
            return result;
        }

        [HttpPut("{id:int}")]
        public ActionResult<SuccessResponse> Update(RatingUpdateRequest model)
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                int userId = _authService.GetCurrentUserId();
                _service.Update(model, userId);

                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
                base.Logger.LogError(ex.ToString());
            }

            return StatusCode(code, response);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<SuccessResponse> Delete(int id)
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                int userId = _authService.GetCurrentUserId();

                _service.Delete(id, userId);

                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
                base.Logger.LogError(ex.ToString());
            }
            return StatusCode(code, response);
        }
    }
}

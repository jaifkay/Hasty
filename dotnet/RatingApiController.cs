using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain.Ratings;
using Sabio.Models.Requests.Ratings;
using Sabio.Services;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Drawing.Printing;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/ratings")]
    [ApiController]
    public class RatingApiController : BaseApiController
    {
        IRatingService _service = null;
        IAuthenticationService<int> _authService = null;

        public RatingApiController (
            IRatingService service, 
            ILogger<RatingApiController> logger, 
            IAuthenticationService<int> auth): base(logger)
        {
            _service = service;
            _authService = auth;
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
        public ActionResult<ItemResponse<int>> Add (RatingAddRequest model)
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

    }
}

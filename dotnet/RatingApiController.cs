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
    }
}

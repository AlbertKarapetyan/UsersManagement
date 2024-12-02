using API.Models;
using AutoMapper;
using Domain.Commands;
using Domain.Queries;
using DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();

                if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer "))
                {
                    return Unauthorized(new { message = "JWT token is missing or invalid." });
                }

                var token = authHeader["Bearer ".Length..];
                var result = await _mediator.Send(new GetUserByTokenQuery(token));
                if (result == null)
                    return Unauthorized();
                else
                    return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();

                if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer "))
                {
                    return Unauthorized(new { message = "JWT token is missing or invalid." });
                }

                var result = await _mediator.Send(new GetAllUsersQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateUsers([FromBody] List<UserUpdateStateModel> modifiedUsers)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();

                if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer "))
                {
                    return Unauthorized(new { message = "JWT token is missing or invalid." });
                }

                if (modifiedUsers == null || modifiedUsers.Count == 0)
                {
                    return BadRequest("No data received.");
                }

                var modifiedUserStates = _mapper.Map<List<UserUpdateStateDTO>>(modifiedUsers);

                var result = await _mediator.Send(new UpdateUsersCommand(modifiedUserStates));

                var token = authHeader["Bearer ".Length..];
                var checkAccess = await _mediator.Send(new GetUserByTokenQuery(token));
                if (checkAccess == null || !checkAccess.IsActive)
                    return Unauthorized();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

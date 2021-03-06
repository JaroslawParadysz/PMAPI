﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.InputDto;
using API.LinksBuildings;
using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserErrorsController : ControllerBase
    {
        public UserErrorsController(
            IUserErrorsRepository userErrorRepository,
            ILinksBuilder linksBuilder)
        {
            _userErrorRepository = userErrorRepository;
            _linksBuilder = linksBuilder;
        }

        public IUserErrorsRepository _userErrorRepository { get; private set; }
        public ILinksBuilder _linksBuilder { get; private set; }

        [HttpPost(Name = "CreateUserError")]
        public async Task<IActionResult> CreateUser(UserErrorForCreationDto userErrorDto)
        {
            UserError userError = UserError.Create(
                userErrorDto.CreatedTime,
                userErrorDto.UpdatedTime,
                userErrorDto.Notes);
            await _userErrorRepository.SaveUserErrors(userError);
            IList<LinkDto> links = _linksBuilder
                .AddLink(
                    Url.Link("GetUserError", new { userError.UserErrorId }),
                    "get-user-error",
                    "GET")
                .AddLink(
                    Url.Link("UpdateUserError", new { userError.UserErrorId }),
                    "update-user-error",
                    "PUT")
                .Build();
            var dto = userError.ShapeData(null);
            ((IDictionary<string, object>)dto).Add("links", links);
            return Ok(dto);
        }

        [HttpGet("{userErrorId}", Name = "GetUserError")]
        public IActionResult GetUserError([FromRoute(Name = "userErrorId")]Guid userErrorId)
        {
            UserError userError = _userErrorRepository.GetUserError(userErrorId);
            if (userError == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            IList<LinkDto> links = CreateLinksForGetMethod(userError);
            var dto = userError.ShapeData(null);
            ((IDictionary<string, object>)dto).Add("links", links);
            return Ok(dto);
        }

        [HttpGet(Name = "GetUserErrors")]
        public async Task<IActionResult> GetUserErrors()
        {
            IEnumerable<UserError> userErrors = await _userErrorRepository.GetUserErrors();
            IList<ExpandoObject> result = new List<ExpandoObject>();

            foreach (UserError userError in userErrors)
            {
                IList<LinkDto> links = CreateLinksForGetMethod(userError);
                ExpandoObject expandoObject = userError.ShapeData(null);
                ((IDictionary<string, object>)expandoObject).Add("links", links);
                result.Add(expandoObject);
            }

            return Ok(result);
        }

        [HttpPut("{userErrorId}", Name = "UpdateUserError")]
        public IActionResult UpdateUserError(
            [FromRoute]Guid userErrorId,
            [FromBody]UserErrorForUpdateDto userErrorForUpdateDto)
        {
            UserError userError = _userErrorRepository.GetUserError(userErrorId);
            if (userError == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }

            userError.Update(userErrorForUpdateDto.CreatedTime, userErrorForUpdateDto.Notes);

            _userErrorRepository.UpdateUserError(userError);
            IList<LinkDto> links = _linksBuilder
            .AddLink(
                Url.Link("GetUserError", new { userError.UserErrorId }),
                "get-user-error",
                "GET")
            .AddLink(
                Url.Link("DeleteUserError", new { userError.UserErrorId }),
                "delete-user-error",
                "DELETE")
            .AddLink(
                Url.Link("UpdateUserError", new { userError.UserErrorId }),
                "self",
                "PUT")
            .Build();

            var dto = userError.ShapeData(null);
            ((IDictionary<string, object>)dto).Add("links", links);
            return Ok(dto);
        }

        [HttpDelete("{userErrorId}", Name = "DeleteUserError")]
        public async Task<IActionResult> DeleteUserError(Guid userErrorId)
        {
            await _userErrorRepository.DeleteUserError(userErrorId);
            return Ok();
        }

        private IList<LinkDto> CreateLinksForGetMethod(UserError userError)
        {
            return _linksBuilder
            .AddLink(
                Url.Link("GetUserError", new { userError.UserErrorId }),
                "self",
                "GET")
            .AddLink(
                Url.Link("DeleteUserError", new { userError.UserErrorId }),
                "delete-user-error",
                "DELETE")
            .AddLink(
                Url.Link("UpdateUserError", new { userError.UserErrorId }),
                "update-user-error",
                "PUT")
            .Build();
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Application.Groups.Commands;
using WebApplication.Application.Groups.Models;
using WebApplication.Application.Groups.Queries;

namespace WebApplication.Controllers
{
    [ApiController]
    [EnableCors("VueCorsPolicy")]
    [Authorize]
    [Route("api/meet-up/group")]
    public class GroupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<GroupDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetGroupsAsync([FromQuery] GetGroupsQuery getGroupsQuery)
        {
            return Ok(await _mediator.Send(getGroupsQuery));
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("{groupId}")]
        [ProducesResponseType(typeof(GroupDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetGroupByIdAsync([FromRoute]string groupId)
        {
            return Ok(await _mediator.Send(new GetGroupQuery { Id = groupId }));
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateGroupAsync([FromBody] CreateGroupCommand createGroupCommand)
        {
            var id = await _mediator.Send(createGroupCommand);
            return Ok(id);
        }

        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteGroupAsync( string groupId)
        {
            return Ok(await _mediator.Send(new DeleteGroupCommand { Id = groupId }));
        }

        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateGroupAsync([FromRoute] string groupId, [FromBody] UpdateGroupCommand updateGroupCommand)
        {
            return Ok(await _mediator.Send(updateGroupCommand));
        }
    }
}
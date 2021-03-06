﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Authorization;
using WebApplication.Mongo.Models;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Meetings.Commands
{
    public class UpdateMeetingCommandHandler : IRequestHandler<UpdateMeetingCommand, string>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateMeetingCommandHandler(
            IMeetingRepository meetingRepository,
            IMapper mapper,
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _meetingRepository = meetingRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
        }
        public async Task<string> Handle(UpdateMeetingCommand request, CancellationToken cancellationToken)
        {
            var meeting = await _meetingRepository.GetMeetingByIdAsync(request.ID);
            _authorizationService.AuthorizeGroupAccessOrThrow(_httpContextAccessor.HttpContext, meeting.GroupID);
            var changes = _mapper.Map<MeetingDO>(request);
            var updated = _mapper.Map(meeting, changes);
            await _meetingRepository.UpdateMeetingAsync(updated);
            return request.ID;
        }
    }
}

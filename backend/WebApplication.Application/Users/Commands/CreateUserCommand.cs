﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Application.Users.Commands
{
    public class CreateUserCommand : IRequest<string>
    {
    }
}

﻿using MediatR;

namespace Scraper.Application.Features.Auth.Commands.Register
{
    public class AuthRegisterCommand:IRequest<AuthRegisterDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}

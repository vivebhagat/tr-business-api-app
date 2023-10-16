﻿using MediatR;

namespace PropertySolutionHub.Application.Users.LeaseAgreementComponent.Command
{
    public class DeleteLeaseAgreementCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}

using Domain.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MessageHandlers.AuthMessages
{
    public class ConfirmEmailRequest : IRequest<string>
    {
        public ConfirmEmailRequest(string id, string code)
        {
            Id = id;
            Code = code;
        }

        public string Id { get; }
        public string Code { get; }

    }
    public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailRequest, string>
    {
        private readonly UserManager<AppUser> _userManager;

        public ConfirmEmailHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<string> Handle(ConfirmEmailRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            await _userManager.ConfirmEmailAsync(user, request.Code);
            return "Ваш аккаунт подтвержден!";
        }
    }
}

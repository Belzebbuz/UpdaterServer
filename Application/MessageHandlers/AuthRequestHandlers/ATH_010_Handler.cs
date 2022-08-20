using Application.DTO.AuthDTO.ATH_010;
using Application.DTO.AuthDTO.ATH_011;
using Application.DTO.AuthDTO.ATH_400;
using Domain.Entities.Auth;

namespace Application.MessageHandlers.AuthRequestHandlers
{
	public class ATH_010_Handler : IRequestHandler<ATH_010, IResponse>
	{
		private readonly IRepository<UserGroup> _userGroupRepository;
		private readonly IRepository<AppUser> _userRepository;

		public ATH_010_Handler(IRepository<UserGroup> userGroupRepository, IRepository<AppUser> userRepository)
		{
			_userGroupRepository = userGroupRepository;
			_userRepository = userRepository;
		}
		public async Task<IResponse> Handle(ATH_010 request, CancellationToken cancellationToken)
		{
			var group = await _userGroupRepository.FirstOrDefaultAsync(new ATH_010_Spec.FindGroupByIdSpec(request.UserGroupId));
			if (group == null)
				return new ATH_400("User group not found!");

			var user = await _userRepository.GetByIdAsync(request.UserId);
			if (user == null)
				return new ATH_400("User not found!");

			group.Users.Add(user);
			await _userGroupRepository.UpdateAsync(group);
			return new ATH_011(group.Id, group.Users.Select(x => x.UserName));
		}
	}
}

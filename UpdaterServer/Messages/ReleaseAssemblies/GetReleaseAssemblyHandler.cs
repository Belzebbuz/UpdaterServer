using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdaterServer.Messages.ReleaseAssemblies
{
	public class GetReleaseAssemblyRequest : IRequest<ReleaseAssembly>
	{
		public Guid Id { get; set; }

		public GetReleaseAssemblyRequest(Guid id)
		{
			Id = id;
		}

	}
	public class GetReleaseAssemblyHandler : IRequestHandler<GetReleaseAssemblyRequest, ReleaseAssembly>
	{
		private readonly AppDbContext _appDbContext;

		public GetReleaseAssemblyHandler(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}
		public async Task<ReleaseAssembly> Handle(GetReleaseAssemblyRequest request, CancellationToken cancellationToken)
		{
			return await _appDbContext.ReleaseAssemblies.FirstOrDefaultAsync(x => x.Id == request.Id);
		}
	}
}

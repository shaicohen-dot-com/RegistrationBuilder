using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using VDT.Core.ServiceModels;

namespace VDT.Core.Interfaces.Services
{
	public interface IInteractionService
	{
		void Create(InteractionModel model);
		Task CreateAsync(InteractionModel model);
		Task<InteractionModel> RetrieveAsync(Guid id);
		Task<IEnumerable<InteractionModel>> RetrieveByIdExternalAsync(IEnumerable<string> idExternal);
		Task<IEnumerable<InteractionModel>> RetrieveByIdExternalAsync(string idExternal);
	}
}
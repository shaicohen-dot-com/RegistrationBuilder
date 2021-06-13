using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Shared.Interactions.ServiceModels;

namespace Shared.Interactions
{
	public interface IInteractionService
	{
		//void Create(InteractionModel model);
		Task CreateAsync(InteractionModel model);
		//Task<InteractionModel> RetrieveAsync(Guid id);
		//Task<IEnumerable<InteractionModel>> RetrieveByIdExternalAsync(IEnumerable<string> idExternal);
		//Task<IEnumerable<InteractionModel>> RetrieveByIdExternalAsync(string idExternal);
	}
}
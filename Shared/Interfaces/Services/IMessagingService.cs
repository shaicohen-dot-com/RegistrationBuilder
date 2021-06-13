using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDT.Api.Mobile.Services.Messaging.Models;
using VDT.Core.ServiceModels;

namespace VDT.Core.Interfaces.Services
{
	public interface IMessagingService
	{
		Task<MessageModel> CreateMessageAsync(Guid idThread, MessageNewModel messageModel, CreatorModel creatorModel, LinkNewModel linkModel = null);
		Task<ThreadModel> CreateThreadAsync(ThreadNewModel threadModel, MessageNewModel messageModel, CreatorModel creatorModel, LinkNewModel linkModel = null);
		Task<MessageModel> GetMessageAsync(Guid id);
		Task<IEnumerable<ThreadModel>> GetMessagesAfterDateAsync(DateTime dateFromUTC, IEnumerable<string> entityTypeIds);
		Task<IEnumerable<MessageModel>> GetMessagesByThreadAsync(Guid idThread);
		Task<IEnumerable<MessageRecipientModel>> GetRecipientsAsync(string mvdid);
		Task<ThreadModel> GetThreadAsync(Guid id);
		Task<IEnumerable<ThreadModel>> GetThreadsByMVDIDAsync(string mvdid);
		Task<IEnumerable<ThreadTopicModel>> GetThreadTopicsAsync(string mvdid);
		Task<bool> SendWelcomeMessageAsync(string mvdid);
		Task<DateTime?> RetrieveMessageReadDateAsync(Guid idMessage, string idUserExternal);

	}
}
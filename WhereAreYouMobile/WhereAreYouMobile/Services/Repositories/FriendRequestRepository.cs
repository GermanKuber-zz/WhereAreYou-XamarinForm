using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Abstractions.Repositories;
using WhereAreYouMobile.Data;
using WhereAreYouMobile.Services.Repositories;
using Xamarin.Forms;
[assembly: Dependency(typeof(FriendRequestRepository))]
namespace WhereAreYouMobile.Services.Repositories
{
    public class FriendRequestRepository : IFriendRequestRepository
    {

        private readonly IDataService _dataService;
        private readonly ILoggerService _loggerService;

        public FriendRequestRepository()
        {
            _dataService = DependencyService.Get<IDataService>();
            _loggerService = DependencyService.Get<ILoggerService>();
        }
        public async Task<bool> SendInvitationAsync(UserProfile profileSend, UserProfile profileReceive)
        {
            try
            {
                if (profileSend == null)
                    throw new ArgumentNullException(nameof(profileSend));
                if (profileReceive == null)
                    throw new ArgumentNullException(nameof(profileReceive));


                var newRequest = new FriendRequest(profileSend, profileReceive);

                //Busco una invitacion entre los mismos destinatarios que este activa o que ya fue acepta (lo que quiere decir que son amigos)
                var invitation =
                    await this._dataService.FriendRequestTable.Where(
                        x => x.IdUserDestinationInvitation == profileReceive.Id
                             && x.IdUserSendInvitation == profileSend.Id
                             &&
                             (x.Status == FriendRequestStatusEnum.Sended
                             || x.Status == FriendRequestStatusEnum.Accepted)).ToEnumerableAsync();

                if (invitation == null || !invitation.Any())
                {
                    //En caso de no encontrar una creo una nueva solicitud de amistad
                    await _dataService.FriendRequestTable.InsertAsync(newRequest);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                await _loggerService.LogErrorAsync(e);
                throw;
            }

        }
        /// <summary>
        /// Si el Id no esta null o empty, intenta actualizar, si esta null o empty inserta
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task SaveAsync(FriendRequest item)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(item.Id))
                    await _dataService.FriendRequestTable.InsertAsync(item);

                else
                    await _dataService.FriendRequestTable.UpdateAsync(item);
            }
            catch (Exception e)
            {
                await _loggerService.LogErrorAsync(e);
                throw;
            }
        }
        /// <summary>
        /// Retorna la lista de invitaciones enviadas por el usuario
        /// </summary>
        /// <param name="idUserSendInvitation"></param>
        /// <returns></returns>
        public async Task<IEnumerable<FriendRequest>> GetSendedRequestsAsync(string idUserSendInvitation)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(idUserSendInvitation))
                    throw new ArgumentNullException(nameof(idUserSendInvitation));

                return await this._dataService.FriendRequestTable.Where(x => x.IdUserSendInvitation == idUserSendInvitation
                                                                       && x.Status != FriendRequestStatusEnum.DeletedFriend)
                    .ToEnumerableAsync();
            }
            catch (Exception e)
            {
                await _loggerService.LogErrorAsync(e);
                throw;
            }

        }

        /// <summary>
        /// Retorna la lista de invitaciones recibidas del usuario
        /// </summary>
        /// <param name="idUserReceived">Id del usuario que recibio todos los request</param>
        /// <returns></returns>
        public async Task<IEnumerable<FriendRequest>> GetAllReceivedRequestsAsync(string idUserReceived)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(idUserReceived))
                    throw new ArgumentNullException(nameof(idUserReceived));

                return await this._dataService.FriendRequestTable.Where(x => x.IdUserDestinationInvitation == idUserReceived
                                                                       && x.Status == FriendRequestStatusEnum.Sended)
                                                                          .ToEnumerableAsync();
            }
            catch (Exception e)
            {
                await _loggerService.LogErrorAsync(e);
                throw;
            }

        }
        public async Task<FriendRequest> GetByUsersIdAsync(string idUser, string idFriend)
        {
            var heInvitation = (await this._dataService.FriendRequestTable.Where(
                    x => (x.IdUserSendInvitation == idUser && x.IdUserDestinationInvitation == idFriend)
                    ||
                    (x.IdUserSendInvitation == idFriend && x.IdUserDestinationInvitation == idUser))
                .ToEnumerableAsync()).SingleOrDefault();
            return heInvitation;
        }

        /// <summary>
        /// Verifico si el idUserMan recibio tiene una invitación del idFriendUser
        /// </summary>
        /// <param name="idUserMain">Id del usuario que recibe la invitacion</param>
        /// <param name="idFriendUser">Id del usuairo que envio la invitación</param>
        /// <returns></returns>
        public async Task<FriendRequest> GetReceivedRequsetAsync(string idUserMain, string idFriendUser)
        {
            var heInvitation = (await this._dataService.FriendRequestTable.Where(
                    x => (x.IdUserSendInvitation == idFriendUser && x.IdUserDestinationInvitation == idUserMain))
                .ToEnumerableAsync()).SingleOrDefault();
            return heInvitation;
        }
        /// <summary>
        /// Verifico si el idUserMan envio  una invitación a idFriendUser
        /// </summary>
        /// <param name="idUserMain">Id del usuario que envía la invitación</param>
        /// <param name="idFriendUser">Id del usuairo que recibe la invitación</param>
        /// <returns></returns>
        public async Task<FriendRequest> GetSendedRequestAsync(string idUserMain, string idFriendUser)
        {
            try
            {
                var invitation = await this._dataService.FriendRequestTable.Where(
                        x => (x.IdUserDestinationInvitation == idFriendUser))
                    .ToListAsync();

                var firsts = invitation.SingleOrDefault();
                return firsts;
            }
            catch (Exception e)
            {
                await _loggerService.LogErrorAsync(e);
                throw;
            }
        }
    }
}

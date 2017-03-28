namespace WhereAreYouMobile.Services.Common
{
    public enum EventAgregatorTypeEnum
    {
        //Evento que avisa que la lista de mis amigos fuero modificada, puede ser que se agrego o se elimino un amigo
        UpdateMyFriends,
  
        UpdateSendedInvitationsFriends,
        //Se actualiza mi ubicación por GPS
        UpdateMyLocation
    }

    public enum EventAgregatorRemoteEnum
    {
        //Evento que avisa que alguno de mis amigos actualizo su ubicación
        UpdateMyFriendsLocations
    }
}
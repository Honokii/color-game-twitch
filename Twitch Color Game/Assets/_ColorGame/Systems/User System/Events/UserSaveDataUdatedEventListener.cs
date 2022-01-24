using UnityEngine.Events;
using WIS.Utils.Events;

namespace ColorGame.User {
    public class UserSaveDataUdatedEventListener : BaseGameEventListener<UserSaveDataUpdatedEventArgs, UserSaveDataUpdatedEvent, UserSaveDataUdatedEventResponse> { }
    
    [System.Serializable]
    public class UserSaveDataUdatedEventResponse : UnityEvent<UserSaveDataUpdatedEventArgs> { }
}
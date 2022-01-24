using System.Collections.Generic;
using WIS.Utils.Events;
using UnityEngine;

namespace ColorGame.User {
    [CreateAssetMenu(fileName = "New UserSaveDataUpdatedEvent", menuName = "Color Game/User/Events/Save Data Updated")]
    public class UserSaveDataUpdatedEvent : BaseGameEvent<UserSaveDataUpdatedEventArgs> { }

    public class UserSaveDataUpdatedEventArgs {
        public List<User> Users;
    }
}
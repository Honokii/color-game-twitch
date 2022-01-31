using System;
using UnityEngine.Events;
using WIS.Utils.Events;

namespace ColorGame {
    public class OnBetListUpdatedEventListener : BaseGameEventListener<OnBetListUpdatedEventArgs, OnBetListUpdatedEvent, OnBetListUpdatedEventResponse> { }

    [Serializable]
    public class OnBetListUpdatedEventResponse : UnityEvent<OnBetListUpdatedEventArgs> { }
}
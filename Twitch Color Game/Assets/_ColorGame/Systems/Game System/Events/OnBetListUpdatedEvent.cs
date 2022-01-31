using System.Collections.Generic;
using UnityEngine;
using WIS.Utils.Events;

namespace ColorGame {
    [CreateAssetMenu(fileName = "New OnBetListUpdatedEvent", menuName = "Color Game/Bet/Events/On Bet List Updated")]
    public class OnBetListUpdatedEvent : BaseGameEvent<OnBetListUpdatedEventArgs> { }

    public class OnBetListUpdatedEventArgs {
        public List<Bet> Bets;
    }
}
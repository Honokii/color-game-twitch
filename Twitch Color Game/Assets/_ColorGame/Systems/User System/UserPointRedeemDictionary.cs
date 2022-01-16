using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColorGame.User {
    public class UserPointRedeemDictionary {

        private Dictionary<string, int> _redeemValueDictionary;

        public UserPointRedeemDictionary() {
            _redeemValueDictionary = new Dictionary<string, int>();
            _redeemValueDictionary.Add("CGPoint:500", 500);
            _redeemValueDictionary.Add("CGPoint:1000", 1000);
            _redeemValueDictionary.Add("CGPoint:2000", 2000);
        }

        public bool IsValidRedeemKey(string redeemKey) {
            return _redeemValueDictionary.ContainsKey(redeemKey);
        }

        public int GetRedeemValue(string redeemKey) {
            if (!IsValidRedeemKey(redeemKey)) {
                return 0;
            }

            return _redeemValueDictionary[redeemKey];
        }
    }
}
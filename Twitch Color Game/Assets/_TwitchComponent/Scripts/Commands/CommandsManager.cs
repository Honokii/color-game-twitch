using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WIS.TwitchComponent.Commands {
    public class CommandsManager : MonoBehaviour {
        private static CommandsManager _manager;
        private List<CommandDictionary> _commandDictionaries = new List<CommandDictionary>();

        private void Awake() {
            _manager = this;
        }

        private void OnDestroy() {
            _manager = null;
        }

        public static void RegisterDictionary(CommandDictionary dict) {
            if (_manager == null) {
                return;
            }

            if (_manager._commandDictionaries.Find(x => x.DictionaryId == dict.DictionaryId)) {
                return;
            }

            _manager._commandDictionaries.Add(dict);
        }

        public static void UnregisterDictionary(CommandDictionary dict) {
            if (_manager == null) {
                return;
            }

            if (!_manager._commandDictionaries.Find(x => x.DictionaryId == dict.DictionaryId)) {
                return;
            }

            var commandDicts = _manager._commandDictionaries;
            int dictIndex = -1;
            int dictCount = commandDicts.Count;

            for (int i = dictCount-1; i >= 0; i--) {
                if (commandDicts[i].DictionaryId == dict.DictionaryId) {
                    dictIndex = i;
                    break;
                }
            }

            if (dictIndex < 0 || dictIndex > dictCount) {
                return;
            }

            _manager._commandDictionaries.RemoveAt(dictIndex);
        }
    }
}
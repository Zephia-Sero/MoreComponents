using System;
using System.Linq;
using System.Collections.Generic;
using PiTung;
using PiTung.Components;
using PiTung.Console;
using UnityEngine;

namespace LWComponentsInTUNG {
    class KBR {
        public static Dictionary<string, bool> activeKeys = new Dictionary<string, bool>();
        public static Dictionary<KeyCode, bool> activeKeys_kc = new Dictionary<KeyCode, bool>();
        public static Dictionary<string, bool> activeKeys_button = new Dictionary<string, bool>();

        public static void processKeys() {
            foreach(string key in activeKeys.Keys.ToArray()) {
                //IGConsole.Log(key+ ", " + Input.GetKey(key));
                activeKeys[key]=Input.GetKey(key);
            }
            foreach(KeyCode key in activeKeys_kc.Keys.ToArray()) {
                activeKeys_kc[key]=Input.GetKey(key);
            }
            foreach(string key in activeKeys_button.Keys.ToArray()) {
                activeKeys_button[key]=Input.GetButton(key);
            }
        }

        //uses unity strings for the keys
        public static bool registerKey(string key) { //returns false if key already exists, true if it's able to add it
            if(activeKeys.ContainsKey(key)) return false;

            activeKeys.Add(key, false);
            return true;
        }
        //uses unity KeyCodes for the keys
        public static bool registerKey(KeyCode key) { //returns false if key already exists, true if it's able to add it
            if(activeKeys_kc.ContainsKey(key)) return false;

            activeKeys_kc.Add(key, false);
            return true;
        }
        //uses unity button strings for keys
        public static bool registerButton(string button) {
            if(activeKeys_button.ContainsKey(button)) return false;
            activeKeys_button.Add(button, false);
            return true;
        }
        public static bool isActive(string key) {
            if(!activeKeys.ContainsKey(key)) {
                IGConsole.Log(new KeyNotFoundException($"'{key}'"+" is not contained in activeKeys dictionary! To register another key, call registerKey(string key) first!"));                throw new KeyNotFoundException(key+" is not contained in activeKeys dictionary! To register another key, call registerKey(string key) first!");
            }
            return activeKeys[key];
        }
        public static bool isActive(KeyCode key) {
            if(!activeKeys_kc.ContainsKey(key))
                IGConsole.Log(new KeyNotFoundException($"'KeyCode.{key.ToString()}'"+" is not contained in activeKeys_kc dictionary! To register another key, call registerKey(KeyCode key) first!"));
            return activeKeys_kc[key];
        }
        public static bool isButtonActive(string button) {
            if(!activeKeys_button.ContainsKey(button))
                IGConsole.Log(new KeyNotFoundException($"'{button}'"+" is not contained in activeKeys_button dictionary! To register another key, call registerButton(string button) first!"));
            return activeKeys_button[button];
        }
    }
}

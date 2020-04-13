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

        public static void processKeys() {
            foreach(string key in activeKeys.Keys.ToArray()) {
                //IGConsole.Log(key+ ", " + Input.GetKey(key));
                activeKeys[key]=Input.GetKey(key);
            }
        }

        //uses unity strings for the keys
        public static bool registerKey(string key) { //returns false if key already exists, true if it's able to add it
            if(activeKeys.ContainsKey(key)) return false;

            activeKeys.Add(key, false);
            return true;
        }
        public static bool isActive(string key) {
            if(!activeKeys.ContainsKey(key))
                throw new KeyNotFoundException(key+" is not contained in activeKeys dictionary! To register another key, call registerKey(string key) first!");
            return activeKeys[key];
        }
    }
}

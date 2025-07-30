using HarmonyLib;
using UnityEngine;

namespace ActionSkillHook
{
    public class ModInit : IModApi
    {
        private static Mod _mod_instance;
        private static Harmony _harmony_instance;

        public static Mod ModInstance => _mod_instance;

        public static Harmony HarmonyInstance
        {
            get
            {
                return _harmony_instance ?? (_harmony_instance = new Harmony("com.reynard.actionskillhook"));
            }
        }
        public void InitMod(Mod mod_instance)
        {
            _mod_instance = mod_instance;
            HarmonyInstance.PatchAll();
                
            Debug.Log("[ActionSkillHook] all patched");                        
        }
    }
}

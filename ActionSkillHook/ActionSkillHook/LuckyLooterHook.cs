using System;
using HarmonyLib;
using UnityEngine;

namespace ActionSkillHook
{
    [HarmonyPatch(typeof(LootManager), "LootContainerOpened")]
    public class LuckyLooterHook
    {
        public static void Postfix(ITileEntityLootable _tileEntity, int _entityIdThatOpenedIt, FastTags<TagGroup.Global> _containerTags)
        {
            if (_tileEntity.bPlayerStorage || _tileEntity.bWasTouched)
                return;

            EntityPlayer player = GameManager.Instance.World.GetEntity(_entityIdThatOpenedIt) as EntityPlayer;
            if (player == null) return;
            
            const string cvar_loot_counter_name = "$ASkill_General_OpenedLootContainers";
            const string cvar_loot_speed_name = "$ASkill_General_LootContainerOpenSpeedBonus";
            
            float current_count = player.Buffs.GetCustomVar(cvar_loot_counter_name);         
            player.Buffs.SetCustomVar(cvar_loot_counter_name, current_count + 1f);
            //Debug.Log($"[LuckyLooterHook] {cvar_loot_counter_name} = {current_count:0}");

            float speed_bonus = Math.Min((current_count + 1f) * 0.00016f, 0.8f);
            player.Buffs.SetCustomVar(cvar_loot_speed_name, speed_bonus);
            //Debug.Log($"[LuckyLooterHook] {cvar_loot_speed_name} = {speed_bonus:0.000}");

        }
    }
}

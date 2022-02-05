﻿using HarmonyLib;
using Database;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using STRINGS;
using System.Reflection;



namespace RoomsExpanded
{
    public class RoomsExpanded_Patches
    {
        [HarmonyPatch(typeof(Db))]
        [HarmonyPatch("Initialize")]
        public class Db_Initialize_Patch
        {
            static bool Patched = false;

            public static void Prefix()
            {
                if (Db_Initialize_Patch.Patched)
                    return;

                // This way of patching RoomTypes constructor is required not to lock english translations in.
                // Credit to Peter Han and asquared314 for helping me with this way of patching.

                Harmony harmony = new Harmony("pether-pg.RoomsExpanded");
                Debug.Log("RoomsExpanded: Trying to get RoomTypes type...");
                Type roomTypesType = Type.GetType("Database.RoomTypes, Assembly-CSharp", false);
                if (roomTypesType == null)
                {
                    Debug.Log("RoomsExpanded: Error - RoomTypes type is null...");
                    return;
                }
                var original = roomTypesType.GetConstructor(new Type[] { typeof(ResourceSet) });
                var prefix = typeof(RoomTypes_Constructor_Patch)?.GetMethod("Prefix");
                var postfix = typeof(RoomTypes_Constructor_Patch)?.GetMethod("Postfix");

                if (original == null || prefix == null || postfix == null)
                    Debug.Log("RoomsExpanded: Error - unable to patch RoomTypes constructor - at least one method is null...");
                else
                {
                    harmony.Patch(original, new HarmonyMethod(prefix), new HarmonyMethod(postfix));
                    Db_Initialize_Patch.Patched = true;
                }
            }
        }

        // This method of patching causes an issue with KLEI's translations of Room Constraints. 
        // Must be patched manually to avoid the issue.
        //[HarmonyPatch(typeof(RoomTypes), MethodType.Constructor, new Type[] { typeof(ResourceSet) })]
        public class RoomTypes_Constructor_Patch
        {
            public static void Prefix()
            {
                LightConstraintPrefix();
                RoomsExpanded_Patches_Agricultural.PrepareModifications();
            }

            public static void Postfix(ref RoomTypes __instance)
            {
                SortingCounter.Init();

                Debug.Log("RoomsExpanded: RoomTypes_Constructor_Patch Postfix");
                RoomsExpanded_Patches_Laboratory.AddRoom(ref __instance);
                RoomsExpanded_Patches_Bathroom.AddRoom(ref __instance);
                RoomsExpanded_Patches_Kitchen.AddRoom(ref __instance);
                RoomsExpanded_Patches_Nursery.AddRoom(ref __instance);
                RoomsExpanded_Patches_NurseryGenetic.AddRoom(ref __instance);
                RoomsExpanded_Patches_Gym.AddRoom(ref __instance);
                RoomsExpanded_Patches_Agricultural.ModifyRoom(ref __instance);
                RoomsExpanded_Patches_Aquarium.AddRoom(ref __instance);
                RoomsExpanded_Patches_Botanical.AddRoom(ref __instance);
                RoomsExpanded_Patches_Graveyard.AddRoom(ref __instance);
                RoomsExpanded_Patches_Hospital.UpdateRoom(ref __instance);
                RoomsExpanded_Patches_Industrial.AddRoom(ref __instance);
                RoomsExpanded_Patches_Museum.AddRoom(ref __instance);
                RoomsExpanded_Patches_MuseumHistory.AddRoom(ref __instance);
                RoomsExpanded_Patches_PrivateRoom.AddRoom(ref __instance);

                // Temporary "Room Size" mod functionality restored for DLC
                // Must be removed once "Room Size" is updated for DLC
                // Original mod by trevis can be found here: https://steamcommunity.com/sharedfiles/filedetails/?id=1715802131
                RoomConstraintTags.ResizeRooms(ref __instance);
            }

            private static void LightConstraintPrefix()
            {
                Func<Room, bool> OriginalCheck = RoomConstraints.LIGHT.room_criteria;
                RoomConstraints.LIGHT = new RoomConstraints.Constraint(
                    (Func<KPrefabID, bool>) null,
                    (Func<Room, bool>) ( 
                        room =>
                        {
                            foreach (KPrefabID plant in room.cavity.plants)
                                if (plant != null && plant.GetComponent<Light2D>() != null && plant.GetComponent<Light2D>().isActiveAndEnabled)
                                    return true;
                            return OriginalCheck(room);
                        }
                    ),
                    name: ((string)ROOMS.CRITERIA.LIGHT.NAME), 
                    description: ((string)ROOMS.CRITERIA.LIGHT.DESCRIPTION)
                    );
            }
        }

        [HarmonyPatch(typeof(RoomProber), MethodType.Constructor)]
        public static class RoomProber_Constructor_Patch
        {
            public static void Postfix()
            {
                // Temporary "Room Size" mod functionality restored for DLC
                // Must be removed once "Room Size" is updated for DLC
                // Original mod by trevis can be found here: https://steamcommunity.com/sharedfiles/filedetails/?id=1715802131
                TuningData<RoomProber.Tuning>.Get().maxRoomSize = Settings.Instance.ResizeMaxRoomSize;
            }
        }


        [HarmonyPatch(typeof(OverlayModes.Rooms))]
        [HarmonyPatch("GetCustomLegendData")]
        public static class Rooms_GetCustomLegendData_Patch
        {
            public static void Postfix(ref List<LegendEntry> __result)
            {
                if (Settings.Instance.HideLegendEffect)
                    foreach (LegendEntry entry in __result)
                        entry.name = entry.name.Split('\n')[0];

                List<RoomType> roomTypeList = new List<RoomType>((IEnumerable<RoomType>)Db.Get().RoomTypes.resources);
                foreach (RoomType roomType in roomTypeList)
                {
                    if (roomType.effects == null && !string.IsNullOrEmpty(roomType.effect))
                    {
                        for (int i = 0; i < __result.Count; i++)
                            if (__result[i].name.Contains(roomType.Name))
                            {
                                string header = (string)ROOMS.EFFECTS.HEADER;
                                __result[i].desc += $"\n\n{header}\n    {roomType.effect}";
                            }
                    }
                }

                if (Settings.Instance.Agricultural.IncludeRoom)
                {
                    LegendEntry farm = __result.Find(entry => ((LegendEntry)entry).name.Contains(ROOMS.TYPES.FARM.NAME));
                    if (farm != null)
                        __result.Remove(farm);
                }
            }
        }
    }
}


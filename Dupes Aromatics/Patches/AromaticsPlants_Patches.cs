﻿using HarmonyLib;
using KMod;
using ProcGen;
using System;
using System.Collections.Generic;
using System.Linq;
using TUNING;
using Dupes_Aromatics.Plants;

namespace Dupes_Aromatics.Patches
{
    public class AromaticsPlants_Patches
    {
        //public static Dictionary<string, CuisinePlantsTuning.CropsTuning> CropsDictionary;
        public const float CyclesForGrowth = 4f;


        [HarmonyPatch(typeof(EntityConfigManager), "LoadGeneratedEntities")]
        public static class EntityConfigManager_LoadGeneratedEntities_Patch
        {
            public static void Prefix()
            {
                //=========================================================================> SPINOSA <===============================
                //====[ SPINOSA ROSE ]===================
                RegisterStrings.MakePlantProductStrings(Crop_SpinosaRoseConfig.ID, STRINGS.CROPS.SPINOSAROSE.NAME, STRINGS.CROPS.SPINOSAROSE.DESC);

                //====[ SPINOSA HIPS ]===================
                RegisterStrings.MakeFoodStrings(Crop_SpinosaHipsConfig.ID, STRINGS.CROPS.SPINOSAHIPS.NAME, STRINGS.CROPS.SPINOSAHIPS.DESC);

                //====[ SPINOSA SEED ]===================
                RegisterStrings.MakeSeedStrings(Plant_SpinosaConfig.SEED_ID, STRINGS.SEEDS.SPINOSA.SEED_NAME, STRINGS.SEEDS.SPINOSA.SEED_DESC);

                //====[ BLOOMING SPINOSA ]===============
                RegisterStrings.MakePlantSpeciesStrings(Plant_SpinosaConfig.ID, STRINGS.PLANTS.SPINOSA.NAME, STRINGS.PLANTS.SPINOSA.DESC);
                CROPS.CROP_TYPES.Add(new Crop.CropVal(Crop_SpinosaRoseConfig.ID, 100f, 1, true));

                //====[ FRUITING SPINOSA ]===============
                RegisterStrings.MakePlantSpeciesStrings(Plant_SuperSpinosaConfig.ID, STRINGS.PLANTS.SUPERSPINOSA.NAME, STRINGS.PLANTS.SUPERSPINOSA.DESC);
                CROPS.CROP_TYPES.Add(new Crop.CropVal(Crop_SpinosaHipsConfig.ID, 100f, 1, true));

                //=========================================================================> DUSK LAVENDER <========================
                //====[ DUSKBLOOM ]===================
                RegisterStrings.MakePlantProductStrings(Crop_DuskbloomConfig.ID, STRINGS.CROPS.DUSKBLOOM.NAME, STRINGS.CROPS.DUSKBLOOM.DESC);

                //====[ DUSKBERRY ]===================
                RegisterStrings.MakeFoodStrings(Crop_DuskberryConfig.ID, STRINGS.CROPS.DUSKBERRY.NAME, STRINGS.CROPS.DUSKBERRY.DESC);

                //====[ DUSK SEED ]===================
                RegisterStrings.MakeSeedStrings(Plant_DuskLavenderConfig.SEED_ID, STRINGS.SEEDS.DUSKLAVENDER.SEED_NAME, STRINGS.SEEDS.DUSKLAVENDER.SEED_DESC);

                //====[ DUSKBLOOM LAVENDER ]==========
                RegisterStrings.MakePlantSpeciesStrings(Plant_DuskLavenderConfig.ID, STRINGS.PLANTS.DUSKLAVENDER.NAME, STRINGS.PLANTS.DUSKLAVENDER.DESC);
                CROPS.CROP_TYPES.Add(new Crop.CropVal(Crop_DuskbloomConfig.ID, 100f, 1, true));

                //====[ DUSKBERRY LAVENDER ]==========
                RegisterStrings.MakePlantSpeciesStrings(Plant_SuperDuskLavenderConfig.ID, STRINGS.PLANTS.SUPERDUSKLAVENDER.NAME, STRINGS.PLANTS.SUPERDUSKLAVENDER.DESC);
                CROPS.CROP_TYPES.Add(new Crop.CropVal(Crop_DuskberryConfig.ID, 100f, 1, true));

                //=========================================================================> RIMED MALLOW <========================
                //====[ RIMED COTTON BOLL ]===========
                RegisterStrings.MakePlantProductStrings(Crop_CottonBollConfig.ID, STRINGS.CROPS.COTTONBOLL.NAME, STRINGS.CROPS.COTTONBOLL.DESC);

                //====[ ICED MALLOW SEED ]============
                RegisterStrings.MakeSeedStrings(Plant_RimedMallowConfig.SEED_ID, STRINGS.SEEDS.RIMEDMALLOW.SEED_NAME, STRINGS.SEEDS.RIMEDMALLOW.SEED_DESC);

                //====[ RIMED MALLOW ]================
                RegisterStrings.MakePlantSpeciesStrings(Plant_RimedMallowConfig.ID, STRINGS.PLANTS.RIMEDMALLOW.NAME, STRINGS.PLANTS.RIMEDMALLOW.DESC);
                CROPS.CROP_TYPES.Add(new Crop.CropVal(Crop_CottonBollConfig.ID, 100f, 1, true));
            }
        }
    }
}

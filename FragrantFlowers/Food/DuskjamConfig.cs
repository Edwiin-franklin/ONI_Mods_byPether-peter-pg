﻿using UnityEngine;
using System.Collections.Generic;

namespace FragrantFlowers
{
    class DuskjamConfig : IEntityConfig
    {
        public const string ID = "Duskjam";
        public static ComplexRecipe recipe;

        public string[] GetDlcIds() => DlcManager.AVAILABLE_EXPANSION1_ONLY;

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }

        public GameObject CreatePrefab()
        {

            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[2]
            {
                new ComplexRecipe.RecipeElement(Crop_DuskberryConfig.ID, 2f),
                new ComplexRecipe.RecipeElement(SimHashes.Sucrose.CreateTag(), 4f)
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement(ID, 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated)
            };
            recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(CraftingTableConfig.ID, (IList<ComplexRecipe.RecipeElement>)ingredients, (IList<ComplexRecipe.RecipeElement>)results), ingredients, results)
            {
                time = TUNING.FOOD.RECIPES.STANDARD_COOK_TIME,
                description = STRINGS.FOOD.DUSKJAM.DESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { CookingStationConfig.ID },
                sortOrder = 1
            };

            EdiblesManager.FoodInfo info = new EdiblesManager.FoodInfo(ID, "EXPANSION1_ID", 2400000f, 3, 255.15f, 277.15f, 19200f, true); // see TUNING.FOOD.FOOD_TYPES.WORMSUPERFOOD
            GameObject looseEntity = EntityTemplates.CreateLooseEntity(ID, STRINGS.FOOD.DUSKJAM.NAME, STRINGS.FOOD.DUSKJAM.DESC, 1f, true, Assets.GetAnim("food_duskjam_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true);
            return EntityTemplates.ExtendEntityToFood(looseEntity, info);
        }
    }
}

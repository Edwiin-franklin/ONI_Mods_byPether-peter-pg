﻿using System;
using Klei.AI;

namespace Dupes_Aromatics
{
    class LavenderSmelling : KMonoBehaviour, ISim4000ms
    {
        [MyCmpGet]
        Effects effects;

        public void Sim4000ms(float dt)
        {
            if (!HasEffect())
                CheckExposure();
        }

        public bool HasEffect()
        {
            return effects.HasEffect(LavenderScent.EFFECT_ID);
        }

        public void CheckExposure()
        {
            int cell = Grid.PosToCell(this.gameObject);
            int count = Grid.DiseaseIdx[cell] == Db.Get().Diseases.GetIndex(LavenderScent.ID) ? Grid.DiseaseCount[cell] : 0;
            if (count > 0)
                ApplyEffect();
        }

        public void ApplyEffect()
        {
            effects.Add(LavenderScent.GetCritterEffect(), true);
            PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, STRINGS.EFFECTS.SMELLEDLAVENDER.NAME, this.gameObject.transform);
        }
    }
}

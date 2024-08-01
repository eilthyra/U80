using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OUA.Hunger {
    public class SurvivalManager : MonoBehaviour
    {
        [Header("Hunger")]
        [SerializeField] private float maxHunger;
        [SerializeField] private float hungerDepletionRate;
        private float currentHunger;
        public float HungerPercent => currentHunger / maxHunger;

        [Header("Thirst")]
        [SerializeField] private float maxThirst;
        [SerializeField] private float thirstDepletionRate;
        private float currentThirst;
        public float ThirstPercent => currentThirst / maxThirst;
        

    }
}

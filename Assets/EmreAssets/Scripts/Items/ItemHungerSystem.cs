using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace OUA.Items
{
    public class HungerSystem : MonoBehaviour
    {
        [SerializeField] private Slider hungerSlider;
        [SerializeField] private float maxHunger = 100f;
        [SerializeField] private float hungerDecreaseRate = 1f;
        private float currentHunger;

        private void Start()
        {
            currentHunger = maxHunger;
            hungerSlider.maxValue = maxHunger;
            hungerSlider.value = currentHunger;
        }

        private void Update()
        {
            DecreaseHunger();

            if (currentHunger <= 0)
            {
                LoseChangeScene();
            }
        }

        private void DecreaseHunger()
        {
            if (currentHunger > 0)
            {
                currentHunger -= hungerDecreaseRate * Time.deltaTime;
                hungerSlider.value = currentHunger;
            }
        }

        public void IncreaseHunger(float amount)
        {
            currentHunger = Mathf.Clamp(currentHunger + amount, 0, maxHunger);
            hungerSlider.value = currentHunger;
        }
        private void LoseChangeScene()
        {
            SceneManager.LoadScene("FailureScene");
        }
    }
}
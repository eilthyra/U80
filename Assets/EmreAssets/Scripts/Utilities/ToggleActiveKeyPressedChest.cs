using UnityEngine;
using UnityEngine.SceneManagement;

namespace OUA.Utilities
{
    public class ToggleActivePressedChest : MonoBehaviour
    {
        [SerializeField] private KeyCode keyCode = KeyCode.None;
        [SerializeField] private GameObject objectToToggle = null;
        [SerializeField] private string playerTag = "Player"; 

        private bool isPlayerInTrigger = false; 

        private void Update()
        {
            if (isPlayerInTrigger && Input.GetKeyDown(keyCode))
            {
                objectToToggle.SetActive(!objectToToggle.activeSelf);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(playerTag))
            {
                isPlayerInTrigger = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(playerTag))
            {
                isPlayerInTrigger = false;
            }
        }
    }
}
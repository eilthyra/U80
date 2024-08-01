using UnityEngine;

namespace OUA.Interactables
{
    public class Interactor : MonoBehaviour
    {
        private IInteractable currentInteractable = null;

        [SerializeField] private GameObject interactionUI; // Etkileşim UI'si için referans

        private void Update()
        {
            CheckForInteraction();
        }

        private void CheckForInteraction()
        {
            if (currentInteractable == null) { return; }

            if (Input.GetKeyDown(KeyCode.E))
            {
                currentInteractable.Interact(transform.root.gameObject);
                currentInteractable = null;
                interactionUI.SetActive(false); // Etkileşim sonrası UI'yi kapat
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var interactable = other.GetComponent<IInteractable>();

            if (interactable == null) { return; }

            currentInteractable = interactable;
            interactionUI.SetActive(true); // Etkileşim alanına girildiğinde UI'yi aç
        }

        private void OnTriggerExit(Collider other)
        {
            var interactable = other.GetComponent<IInteractable>();

            if (interactable == null) { return; }

            if (interactable != currentInteractable) { return; }

            currentInteractable = null;
            interactionUI.SetActive(false); // Etkileşim alanından çıkıldığında UI'yi kapat
        }
    }
}

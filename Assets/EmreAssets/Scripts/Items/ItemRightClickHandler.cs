using OUA.Events.CustomEvents;
using OUA.Items.Inventories;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace OUA.Items
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ItemRightClickHandler : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected ItemSlotUI itemSlotUI = null;
        [SerializeField] protected ItemEvent onMouseClickStartItem = null;
        [SerializeField] protected VoidEvent onMouseClickEndItem = null;

        private CanvasGroup canvasGroup = null;
        private bool isHovering = false;

        public ItemSlotUI ItemSlotUI => itemSlotUI;

        private void Start() => canvasGroup = GetComponent<CanvasGroup>();

        private void OnDisable()
        {
            if (isHovering)
            {
                onMouseClickEndItem.Raise();
                isHovering = false;
            }
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                onMouseClickEndItem.Raise();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                onMouseClickStartItem.Raise(ItemSlotUI.SlotItem);
                isHovering = true;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                onMouseClickEndItem.Raise();
                isHovering = false;
            }
        }
    }
}
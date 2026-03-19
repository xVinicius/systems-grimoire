using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SystemsGrimoire.MVC.Example {
    /// <summary>
    /// Concrete View for a shop screen.
    /// Owns all UI references and exposes events for user interactions.
    /// The View never talks to the Model directly — the Controller mediates.
    /// </summary>
    public class ShopView : MonoBehaviour {
        [SerializeField] private TMP_Text m_SelectedItemNameText;
        [SerializeField] private TMP_Text m_SelectedItemDescriptionText;
        [SerializeField] private TMP_Text m_SelectedItemPriceText;
        [SerializeField] private Button m_PurchaseButton;
        [SerializeField] private Button m_CloseButton;

        public event Action<int> OnItemSelected = delegate { };
        public event Action<int> OnPurchaseRequested = delegate { };

        private int m_CurrentSelectedIndex = -1;

        public void AddItemSelectionListener(Action<int> callback) => OnItemSelected += callback;
        public void RemoveItemSelectionListener(Action<int> callback) => OnItemSelected -= callback;
        public void AddPurchaseListener(Action<int> callback) => OnPurchaseRequested += callback;
        public void RemovePurchaseListener(Action<int> callback) => OnPurchaseRequested -= callback;

        private void Start() {
            m_PurchaseButton.onClick.AddListener(() => {
                if (m_CurrentSelectedIndex >= 0) {
                    OnPurchaseRequested?.Invoke(m_CurrentSelectedIndex);
                    m_PurchaseButton.interactable = false;
                }
            });
        }

        public void UpdateItems(IList<ShopItem> items) {
            // Populate your item list UI here (instantiate prefabs, etc.)
        }

        public void ShowItemDetails(ShopItem item, int index) {
            m_CurrentSelectedIndex = index;
            m_SelectedItemNameText.text = item.Data.Name;
            m_SelectedItemDescriptionText.text = item.Data.Description;
            m_SelectedItemPriceText.text = item.Data.Cost.ToString("N0");
            m_PurchaseButton.gameObject.SetActive(item.Data.IsLocked);
            m_PurchaseButton.interactable = item.Data.IsLocked;
        }

        public void UpdateItem(ShopItem item, int index) {
            // Update a single item slot in the UI
        }

        private void OnDestroy() {
            m_CloseButton.onClick.RemoveAllListeners();
            m_PurchaseButton.onClick.RemoveAllListeners();
        }
    }
}

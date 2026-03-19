using System;
using System.Linq;
using UnityEngine;

namespace SystemsGrimoire.MVC.Example {
    /// <summary>
    /// Concrete Controller wiring ShopModel to ShopView.
    /// Demonstrates the full MVC + Observable pattern:
    /// - Model changes fire ObservableList events -> Controller updates View
    /// - View user input fires events -> Controller updates Model
    ///
    /// Includes a Builder for fluent construction.
    /// </summary>
    public class ShopController : Controller<ShopModel, ShopView> {
        public ShopController(ShopModel model, ShopView view) : base(model, view) { }

        protected override void ConnectModel() {
            m_Model.Items.OnSpecificValueChanged += OnItemChanged;
        }

        protected override void ConnectView() {
            m_View.UpdateItems(m_Model.Items);

            if (m_Model.Count > 0)
                m_View.ShowItemDetails(m_Model.Items.First(), 0);

            m_View.AddItemSelectionListener(OnItemSelected);
            m_View.AddPurchaseListener(OnPurchaseRequested);
        }

        private void OnItemChanged(ShopItem item, int index) {
            m_View.UpdateItem(item, index);
        }

        private void OnItemSelected(int index) {
            var item = m_Model.Items[index];
            if (item != null)
                m_View.ShowItemDetails(item, index);
        }

        private void OnPurchaseRequested(int index) {
            try {
                var item = m_Model.Items[index];
                Debug.Log($"[ShopController] Purchasing: {item.Data.Name} for {item.Data.Cost}");

                // TODO: Call your purchase service here
                // On success, update the model:
                item.Data.IsLocked = false;
                m_Model.Items[index] = item; // triggers OnSpecificValueChanged
                m_View.ShowItemDetails(item, index);
            }
            catch (Exception e) {
                Debug.LogError($"[ShopController] Purchase failed: {e.Message}");
            }
        }

        public override void Destroy() {
            m_Model.Items.OnSpecificValueChanged -= OnItemChanged;
            m_View.RemoveItemSelectionListener(OnItemSelected);
            m_View.RemovePurchaseListener(OnPurchaseRequested);
        }

        /// <summary>
        /// Builder pattern ensures a Controller is always constructed with valid Model + View.
        /// </summary>
        public class Builder {
            private readonly ShopModel m_Model = new();

            public Builder WithItems(ShopItemData[] items) {
                foreach (var item in items)
                    m_Model.Add(new ShopItem(item));
                return this;
            }

            public ShopController Build(ShopView view) {
                return new ShopController(m_Model, view);
            }
        }
    }
}

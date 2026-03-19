using UnityEngine;

namespace SystemsGrimoire.UIFramework {
    /// <summary>
    /// Abstract base class for all UI panels.
    /// Handles show/hide lifecycle, parent state management, and open/close events.
    ///
    /// Derive from this to create your own UI base classes (e.g., MainUI, PopupUI, etc.)
    /// </summary>
    public abstract class UserInterface : MonoBehaviour {
        private bool m_WasParentActive;
        private Transform m_OriginalParent;

        public event OnUIEvent OnUIOpened;
        public event OnUIEvent OnUIClosed;
        public delegate void OnUIEvent(UserInterface sender);

        public abstract void Initialize();

        public virtual void Show() {
            OnUIOpened?.Invoke(this);

            m_OriginalParent = transform.parent;
            if (m_OriginalParent != null) {
                m_WasParentActive = m_OriginalParent.gameObject.activeSelf;

                if (!m_WasParentActive)
                    m_OriginalParent.gameObject.SetActive(true);
            }

            gameObject.SetActive(true);
        }

        public virtual void Hide() {
            OnUIClosed?.Invoke(this);
            gameObject.SetActive(false);

            if (m_OriginalParent != null && !m_WasParentActive)
                m_OriginalParent.gameObject.SetActive(m_WasParentActive);
        }
    }
}

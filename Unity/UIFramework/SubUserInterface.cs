using UnityEngine;

namespace SystemsGrimoire.UIFramework {
    /// <summary>
    /// Base class for overlay / modal UI panels (popups, tooltips, dialogs).
    /// Supports priority tags so that showing a new sub-UI with the same priority
    /// automatically dismisses the previous one of that type.
    /// </summary>
    public class SubUserInterface : UserInterface {
        public enum PriorityTag {
            None,
            Modal,
            Popup,
            Error
        }

        [field: SerializeField] public PriorityTag Priority { get; private set; }

        [Header("Debug")]
        [SerializeField] private bool m_DisplayLogs = false;

        public override void Initialize() {
            if (m_DisplayLogs)
                Debug.Log($"{transform.name} (Sub UI) Initialized");
        }

        public override void Show() {
            if (m_DisplayLogs)
                Debug.Log($"{transform.name} (Sub UI) Shown");
            base.Show();
        }

        public override void Hide() {
            if (m_DisplayLogs)
                Debug.Log($"{transform.name} (Sub UI) Hidden");
            base.Hide();
        }
    }
}

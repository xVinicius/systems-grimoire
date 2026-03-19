using UnityEngine;

namespace SystemsGrimoire.UIFramework {
    /// <summary>
    /// Base class for full-screen / main UI panels (e.g., inventory screen, settings menu).
    /// These represent top-level screens that are tracked in a navigation history stack.
    /// </summary>
    public class MainUserInterface : UserInterface {
        [Header("Debug")]
        [SerializeField] private bool m_DisplayLogs = false;

        public override void Initialize() {
            if (m_DisplayLogs)
                Debug.Log($"{transform.name} (Main UI) Initialized");
        }

        public override void Show() {
            if (m_DisplayLogs)
                Debug.Log($"{transform.name} (Main UI) Shown");
            base.Show();
        }

        public override void Hide() {
            if (m_DisplayLogs)
                Debug.Log($"{transform.name} (Main UI) Hidden");
            base.Hide();
        }
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SystemsGrimoire.UIFramework {
    /// <summary>
    /// Paginated horizontal carousel for UI items.
    /// Supports left/right navigation with smooth lerp animation and automatic button state management.
    ///
    /// Setup:
    /// - Assign a HorizontalLayoutGroup as Content
    /// - Assign Left/Right navigation buttons
    /// - Set Pagination to control how many items scroll per click
    /// - Override for custom carousel behavior (e.g., adding page indicators)
    /// </summary>
    public class UICarousel : MonoBehaviour {
        [field: SerializeField] public HorizontalLayoutGroup Content { get; private set; }
        [field: SerializeField] public Button LeftButton { get; private set; }
        [field: SerializeField] public Button RightButton { get; private set; }

        /// <summary>The current scroll index.</summary>
        [field: SerializeField] public int CurrentIndex { get; private set; } = 0;

        /// <summary>Number of items to scroll per navigation click.</summary>
        [field: SerializeField] public int Pagination { get; private set; } = 1;

        /// <summary>Lerp completion threshold (0-1). 1 = must reach exact target.</summary>
        [field: SerializeField] public float ThresholdInPercent { get; private set; } = 1f;

        /// <summary>Duration of the scroll animation in seconds.</summary>
        [field: SerializeField] public float MoveDuration { get; private set; } = 0.05f;

        /// <summary>Optional: set initial focus for gamepad/keyboard navigation.</summary>
        [field: SerializeField] public GameObject InitialFocus { get; private set; }

        protected float m_ElementWidth;
        protected int m_ContentLength = 0;
        protected float m_Spacing;
        protected Vector2 m_InitialPosition;
        protected RectTransform m_RectTransform;

        protected bool m_Lerping = false;
        protected float m_LerpStartedTimestamp;
        protected Vector2 m_StartPosition;
        protected Vector2 m_TargetPosition;

        protected virtual void Start() {
            Initialization();
        }

        protected virtual void Initialization() {
            m_RectTransform = Content.gameObject.GetComponent<RectTransform>();
            m_InitialPosition = m_RectTransform.anchoredPosition;

            m_ContentLength = 0;
            foreach (Transform tr in Content.transform) {
                m_ElementWidth = tr.gameObject.GetComponent<RectTransform>().sizeDelta.x;
                m_ContentLength++;
            }

            m_Spacing = Content.spacing;
            m_RectTransform.anchoredPosition = DeterminePosition();

            if (InitialFocus != null)
                EventSystem.current.SetSelectedGameObject(InitialFocus, null);
        }

        public virtual void MoveLeft() {
            if (!CanMoveLeft()) return;
            CurrentIndex -= Pagination;
            MoveToCurrentIndex();
        }

        public virtual void MoveRight() {
            if (!CanMoveRight()) return;
            CurrentIndex += Pagination;
            MoveToCurrentIndex();
        }

        protected virtual void MoveToCurrentIndex() {
            m_StartPosition = m_RectTransform.anchoredPosition;
            m_TargetPosition = DeterminePosition();
            m_Lerping = true;
            m_LerpStartedTimestamp = Time.time;
        }

        protected virtual Vector2 DeterminePosition() {
            return m_InitialPosition - (Vector2.right * CurrentIndex * (m_ElementWidth + m_Spacing));
        }

        public virtual bool CanMoveLeft() => CurrentIndex - Pagination >= 0;

        public virtual bool CanMoveRight() => CurrentIndex + Pagination < m_ContentLength;

        protected virtual void Update() {
            if (m_Lerping)
                LerpPosition();
            HandleButtons();
        }

        protected virtual void HandleButtons() {
            if (LeftButton != null)
                LeftButton.gameObject.SetActive(CanMoveLeft());
            if (RightButton != null)
                RightButton.gameObject.SetActive(CanMoveRight());
        }

        protected virtual void LerpPosition() {
            float timeSinceStarted = Time.time - m_LerpStartedTimestamp;
            float percentageComplete = timeSinceStarted / MoveDuration;

            m_RectTransform.anchoredPosition = Vector2.Lerp(m_StartPosition, m_TargetPosition, percentageComplete);

            if (percentageComplete >= ThresholdInPercent)
                m_Lerping = false;
        }
    }
}

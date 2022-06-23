using DG.Tweening;
using TMPro;
using UnityEngine;

namespace ByteTyper
{
    public class MainMenuPanel : MonoBehaviour
    {
        #region Fields

        public TextMeshProUGUI TapToStartText;

		private Sequence m_AnimationSequence;

        #endregion

        #region Properties
        
        #endregion

        #region Unity Methods

        void Awake()
        {
            
        }

        void Start()
        {
        
        }

        #endregion

        #region Public Methods
        public void AnimateStart()
        {
            TapToStartText.gameObject.SetActive(true);

            AnimateTapToStart();

        }

        public void AnimateTapToStart()
        {
            TapToStartText.DOKill();
            TapToStartText.fontSize = 86;
            //TapToStartText.DoFontSize(100, 0.9f).SetLoops(-1, LoopType.Yoyo);
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
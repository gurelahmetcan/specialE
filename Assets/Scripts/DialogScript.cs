using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ByteTyper
{
    public class DialogScript : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GetText GetText;

        [SerializeField] private float typingSpeed = 0.05f;
        
        [SerializeField] private TextMeshProUGUI egeDialogueText;
        [SerializeField] private TextMeshProUGUI gizemDialogueText;
        
        [Header("Animation Controller")]
        [SerializeField] private Animator playEgeAnimator;
        [SerializeField] private Animator playGizemAnimator;
        [SerializeField] private Animator egeBubbleAnimator;
        [SerializeField] private Animator gizemBubbleAnimator;

        [Header("Dialogue Sentences")]
        [TextArea]
        [SerializeField] private String[] egeDialogueSentences;
        [TextArea]
        [SerializeField] private String[] gizemDialogueSentences;
        
        private bool m_IsFirstPress;
        private float m_AnimationDelay = 0.6f;
        
        private Dictionary<int, List<string>> mDictionary = new Dictionary<int, List<string>>();
        
        private int m_index = 0;

        private bool m_isAlreadyOpen;

        private bool m_isEgeTurn;

        private bool m_isAgain;

        #endregion

        #region Properties
        
        #endregion

        #region Unity Methods

        void Awake()
        {
            
        }

        void Start()
        {
            mDictionary = GetText.ReadData();

            m_IsFirstPress = true;
        }

        #endregion

        #region Public Methods
        
        public void TriggerContinue()
        {
            StartCoroutine(OnContinuePressed());
        }
        
        #endregion

        #region Private Methods
        
        private IEnumerator TypeSentence(TMP_Text text, string sentence)
        {
            foreach (char letter in sentence)
            {
                text.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }
        
        private IEnumerator OnContinuePressed()
        {
            var line = mDictionary[m_index];

            if (!m_IsFirstPress)
            {
                if (m_isEgeTurn)
                {
                    m_isAgain = line[0] == "Ege:";
                }
                else
                {
                    m_isAgain = line[0] == "Gizem:";
                }
            }
            
            if (line[0] == "Ege:")
            {
                m_isEgeTurn = true;
                gizemDialogueText.text = string.Empty;

                if (!m_IsFirstPress && !m_isAgain)
                {
                    gizemBubbleAnimator.SetTrigger("Close");
                }
                
                yield return new WaitForSeconds(m_AnimationDelay);

                egeDialogueText.text = string.Empty;
                
                playEgeAnimator.SetTrigger("Talk");

                if (!m_isAgain)
                {
                    egeBubbleAnimator.SetTrigger("Open");
                    yield return new WaitForSeconds(m_AnimationDelay);
                }
                
                egeDialogueText.text = string.Empty;
                StartCoroutine(TypeSentence(egeDialogueText, line[1]));
            }
            else
            {
                m_isEgeTurn = false;
                egeDialogueText.text = string.Empty;
                
                if (!m_IsFirstPress && !m_isAgain)
                {
                    egeBubbleAnimator.SetTrigger("Close");
                }
                
                yield return new WaitForSeconds(m_AnimationDelay);

                gizemDialogueText.text = string.Empty;
                
                playGizemAnimator.SetTrigger("Talk");

                if (!m_isAgain)
                {
                    gizemBubbleAnimator.SetTrigger("Open");
                
                    yield return new WaitForSeconds(m_AnimationDelay);
                }
                
                gizemDialogueText.text = string.Empty;
                StartCoroutine(TypeSentence(gizemDialogueText, line[1]));
            }

            m_IsFirstPress = false;
            m_index++;
        }

        #endregion
    }
}
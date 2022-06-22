using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ByteTyper
{
    public class DialogScript : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float typingSpeed = 0.05f;

        [SerializeField] private bool isFirstSpeaking;

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

        private int egeIndex;
        private int gizemIndex ;

        private bool m_IsFirstPress;

        private float m_AnimationDelay = 0.6f;

        private bool m_EgeTurn;
        
        #endregion

        #region Properties
        
        #endregion

        #region Unity Methods

        void Awake()
        {
            
        }

        void Start()
        {
            m_EgeTurn = isFirstSpeaking;
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
        
        private IEnumerator TypeSentence(TMP_Text text, String[] sentence, int index)
        {
            foreach (char letter in sentence[index])
            {
                text.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }
        
        private IEnumerator OnContinuePressed()
        {
            if (m_EgeTurn)
            {
                if (egeIndex >= egeDialogueSentences.Length - 1)
                {
                    gizemDialogueText.text = string.Empty;
                    
                    gizemBubbleAnimator.SetTrigger("Close");
                }
                else
                {
                    gizemDialogueText.text = string.Empty;

                    if (!m_IsFirstPress)
                    {
                        gizemBubbleAnimator.SetTrigger("Close");
                    }

                    yield return new WaitForSeconds(m_AnimationDelay);

                    egeDialogueText.text = string.Empty;
                
                    playEgeAnimator.SetTrigger("Talk");
                
                    egeBubbleAnimator.SetTrigger("Open");
                
                    yield return new WaitForSeconds(m_AnimationDelay);
                
                    if (egeIndex < egeDialogueSentences.Length)
                    {
                        egeDialogueText.text = string.Empty;
                        StartCoroutine(TypeSentence(egeDialogueText, egeDialogueSentences, egeIndex));
                        egeIndex++;
                        m_EgeTurn = false;
                    }
                }

            }
            else
            {
                if (gizemIndex >= gizemDialogueSentences.Length - 1)
                {
                    egeDialogueText.text = string.Empty;
                    
                    egeBubbleAnimator.SetTrigger("Close");
                }
                else
                {
                    egeDialogueText.text = string.Empty;
                
                    if (!m_IsFirstPress)
                    {
                        egeBubbleAnimator.SetTrigger("Close");
                    }
                
                    yield return new WaitForSeconds(m_AnimationDelay);

                    gizemDialogueText.text = string.Empty;
                
                    playGizemAnimator.SetTrigger("Talk");

                    gizemBubbleAnimator.SetTrigger("Open");
                
                    yield return new WaitForSeconds(m_AnimationDelay);
                
                    if (gizemIndex < gizemDialogueSentences.Length)
                    {
                        gizemDialogueText.text = string.Empty;
                        StartCoroutine(TypeSentence(gizemDialogueText, gizemDialogueSentences, gizemIndex));
                        gizemIndex++;
                        m_EgeTurn = true;
                    }
                }
            }

            m_IsFirstPress = false;
        }

        #endregion
    }
}
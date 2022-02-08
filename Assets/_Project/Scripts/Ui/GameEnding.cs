using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.UI;

public class GameEnding : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] private float fadeDuration = 1f;
    [SerializeField] [Range(1, 2)] private float displayImageDuration = 1f;
    private float m_Timer;

    private bool m_IsPlayerAtExit;
    private bool m_IsPlayerCaught;
    private bool m_HasAudioPlayed;    

    public CanvasGroup exitBackgroundImageCanvasGroup;
    public CanvasGroup caugthBackgroundImageCanvasGroup;

    public AudioSource exitAudio;
    public AudioSource caughtAudio;

    public GameObject player;
    public GameObject uiController;
    public GameObject pauseButton;
    public GameObject resumeButton;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }
    }

    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }

    private void Update()
    {
        if (m_IsPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if (m_IsPlayerCaught)
        {
            EndLevel(caugthBackgroundImageCanvasGroup, true, caughtAudio);
        }
    }

    private void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }

        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            uiController.SetActive(true);
        }
    }

    public void ExitGame()
    {
        MenuManager.instance.ExitGame();
    }

    public void RestartGame()
    {
        MenuManager.instance.StartGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseButton.SetActive(false);
        resumeButton.SetActive(true);
        AudioListener.pause = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseButton.SetActive(true);
        resumeButton.SetActive(false);
        AudioListener.pause = false;
    }
}

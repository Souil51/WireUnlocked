using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMusicController : MonoBehaviour
{
    private static AudioSource m_audioSource;

    private static bool bInit = false;

    private bool bSoundActive = true;

    public void Awake()
    {
        if (!bInit)
        {
            DontDestroyOnLoad(transform.gameObject);
            m_audioSource = GetComponent<AudioSource>();

            bInit = true;

            this.Play();
        }
        else
        {
            Destroy(transform.gameObject);
        }
    }

    public void Play()
    {
        if (m_audioSource.isPlaying) return;

        m_audioSource.Play();

        bSoundActive = true;
    }

    public void Stop()
    {
        m_audioSource.Stop();

        bSoundActive = false;
    }

    public void SetVolume(float fVolume)
    {
        m_audioSource.volume = fVolume * 0.25f;
    }

    public bool IsSoundActive()
    {
        return bSoundActive;
    }
}

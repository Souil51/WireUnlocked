using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeakerSoundController : MonoBehaviour
{
    Button btnSpeaker;

    public Sprite sprite_speaker_on;
    public Sprite sprite_speaker_off;

    MainMusicController mmcSound;

    // Start is called before the first frame update
    void Start()
    {
        mmcSound = GameObject.FindGameObjectWithTag("MainMusic").GetComponent<MainMusicController>();

        btnSpeaker = transform.Find("Button").GetComponent<Button>();

        Image img = btnSpeaker.gameObject.GetComponent<Image>();
        img.sprite = sprite_speaker_on;

        UpdateSoundSpeaker();
    }

    public void ToggleSound()
    {
        if (mmcSound.IsSoundActive())
        {
            Image img = btnSpeaker.gameObject.GetComponent<Image>();
            img.sprite = sprite_speaker_off;

            GameObject.FindGameObjectWithTag("MainMusic").GetComponent<MainMusicController>().Stop();
        }
        else
        {
            Image img = btnSpeaker.gameObject.GetComponent<Image>();
            img.sprite = sprite_speaker_on;

            GameObject.FindGameObjectWithTag("MainMusic").GetComponent<MainMusicController>().Play();
        }
    }

    public void UpdateSoundSpeaker()
    {
        if (mmcSound.IsSoundActive())
        {
            Image img = btnSpeaker.gameObject.GetComponent<Image>();
            img.sprite = sprite_speaker_on;
        }
        else
        {
            Image img = btnSpeaker.gameObject.GetComponent<Image>();
            img.sprite = sprite_speaker_off;
        }
    }
}

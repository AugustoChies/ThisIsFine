using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    public GlobalInfo info;
    public AudioMixer audioMixer;
    public AudioSource victorySong, defeatSong;
    bool playedwin, playeddefeat;
    private bool isFrenetic = false;

    private const string AMBIENT_MUSIC_VOLUME_TAG = "AmbientMusicVolume";
    private const string FRENETIC_MUSIC_VOLUME_TAG = "FreneticMusicVolume";

    public void SetFrenetic(bool frenetic)
    {
        if (!isFrenetic && frenetic)
        {
            StartCoroutine(FadeMusic(AMBIENT_MUSIC_VOLUME_TAG, FRENETIC_MUSIC_VOLUME_TAG, 2));
            isFrenetic = true;
        }
        else if (isFrenetic && !frenetic)
        {
            StartCoroutine(FadeMusic(FRENETIC_MUSIC_VOLUME_TAG, AMBIENT_MUSIC_VOLUME_TAG, 2));
            isFrenetic = false;
        }
    }

    private void Start()
    {
        audioMixer.SetFloat(AMBIENT_MUSIC_VOLUME_TAG, 0);
        audioMixer.SetFloat(FRENETIC_MUSIC_VOLUME_TAG, -80);
    }

    void Update()
    {
        if(info.defeat)
        {
            if(!playeddefeat && !playedwin)
            {
                playeddefeat = true;
                defeatSong.Play();
                audioMixer.SetFloat(AMBIENT_MUSIC_VOLUME_TAG, -80);
                audioMixer.SetFloat(FRENETIC_MUSIC_VOLUME_TAG, -80);
            }
        }

        if(info.victory)
        {
            if (!playeddefeat && !playedwin)
            {
                playedwin = true;
                victorySong.Play();
                audioMixer.SetFloat(AMBIENT_MUSIC_VOLUME_TAG, -80);
                audioMixer.SetFloat(FRENETIC_MUSIC_VOLUME_TAG, -80);
            }
        }
    }

    private IEnumerator FadeMusic(string currentMusicVolumeTag, string targetMusicVolumeTag, float duration)
    {
        float currentTime = 0;
        float currentVol;
        audioMixer.GetFloat(currentMusicVolumeTag, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(0, 0.0001f, 1);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            audioMixer.SetFloat(currentMusicVolumeTag, Mathf.Log10(newVol) * 20);
            audioMixer.SetFloat(targetMusicVolumeTag, Mathf.Log10(1 - newVol) * 20);
            yield return null;
        }
        yield break;
    }
}

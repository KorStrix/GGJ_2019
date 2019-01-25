using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGMSounds
{
    NONE = 0,
    MAIN_THEME,
}

public enum SFXSounds
{
    NONE = 0,
    JUMP,
}

public class SoundManager : MonoBehaviour
{
    public const float FADE_TIME = 2f;

    private List<Sound> BGMQueue = new List<Sound>();
    private bool BGMRunning = false;
    private List<Sound> SFXQueue = new List<Sound>();
    private bool SFXRunning = false;

    public void StartBGMQueue()
    {
        if (BGMRunning)
        {
            return;
        }
        if (BGMQueue.Count > 0)
        {
            StartCoroutine(IRunBGMQueue());
        }
    }

    public void StartSFXQueue()
    {
        if (SFXRunning)
        {
            return;
        }
        if (SFXQueue.Count > 0)
        {
            StartCoroutine(IRunSFXQueue());
        }
    }

    public void AddBGMQueue(Sound s)
    {
        foreach(Sound t in BGMQueue)
        {
            if (s.bgm == t.bgm)
            {
                return;
            }
        }
        BGMQueue.Add(s);
    }

    public void AddSFXQueue(Sound s)
    {
        BGMQueue.Add(s);
    }

    public void RunBGMQueue()
    {
        StartCoroutine(IRunBGMQueue());
    }

    IEnumerator IRunBGMQueue()
    {
        BGMRunning = true;
        while (BGMQueue.Count > 0)
        {
            BGMQueue[0].Ticking();
            if (BGMQueue[0].lifeTime < 0f)
            {
                BGMQueue.RemoveAt(0);
                Debug.Log("BGM Removed");
            }
            yield return null;
        }
        BGMRunning = false;
    }

    public void RunSFXQueue()
    {
        StartCoroutine(IRunSFXQueue());
    }

    IEnumerator IRunSFXQueue()
    {
        SFXRunning = true;
        while (SFXQueue.Count > 0)
        {
            SFXQueue[0].Ticking();
            if (SFXQueue[0].lifeTime < 0f)
            {
                SFXQueue.RemoveAt(0);
                Debug.Log("SFX Removed");
            }
            yield return null;
        }
        SFXRunning = false;
    }

    //public void FadeIn();
    //IEnumerator FadeIn();

    //public void FadeOut();
    //IEnumerator FadeOut();

}
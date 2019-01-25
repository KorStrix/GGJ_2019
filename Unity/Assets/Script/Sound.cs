using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound
{
    public BGMSounds bgm = BGMSounds.NONE;
    public SFXSounds sfx = SFXSounds.NONE;
    public float lifeTime = 0f;
    private bool hasPlayed = false;

    public Sound(BGMSounds _bgm, float time)
    {
        bgm = _bgm;
        lifeTime = time;
    }

    public Sound(SFXSounds _sfx, float time)
    {
        sfx = _sfx;
        lifeTime = time;
    }

    public void FireBGM()
    {
        //switch case
    }

    public void FireSFX()
    {
        //switch case

        /*
        switch (sfx)
        {
        case SFXSounds.JUMP:
            ok.GetComponent<AudioSource>().Play();
            break;
        default:
            break;
        }
    */
    }

    public void Ticking()
    {
        lifeTime -= Time.deltaTime;
        if (hasPlayed)
            return;
        
        hasPlayed = true;
        if (bgm != BGMSounds.NONE)
        {
            FireBGM();
            return;
        }
        FireSFX();
    }
}
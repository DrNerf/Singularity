using UnityEngine;
using System.Collections;

public class ButtonsController : MonoBehaviour 
{
    public Texture DefaultTexture;
    public Texture HoverTexture;
    public bool LoadingLvl = false;
    public int LvlId;
    public AudioSource ButtonsAudioSource;
    public AudioClip HoverSound;
    public AudioClip ActivateSound;

    void Start()
    {
        renderer.material.SetTexture("_MainTex", DefaultTexture);
    }
    void OnMouseEnter()
    {
        renderer.material.SetTexture("_MainTex", HoverTexture);
        PlayHover();
    }

    void OnMouseExit()
    {
        renderer.material.SetTexture("_MainTex", DefaultTexture);
    }

    void OnMouseDown()
    {
        PlayActivate();
        Invoke("LoadLvl", 0.3f);
    }

    void PlayHover()
    {
        ButtonsAudioSource.clip = HoverSound;
        ButtonsAudioSource.Play();
    }

    void PlayActivate()
    {
        ButtonsAudioSource.Stop();
        ButtonsAudioSource.clip = ActivateSound;
        ButtonsAudioSource.Play();
    }

    void LoadLvl()
    {
        if (LoadingLvl)
        {
            Application.LoadLevel(LvlId);
        }
        else
        {
            Application.Quit();
        }
    }
}

using UnityEngine;
using System.Collections;

public class CreditsController : MonoBehaviour {

    public Texture DefaultTexture;
    public Texture HoverTexture;
    public AudioSource ButtonsAudioSource;
    public AudioClip HoverSound;
    public AudioClip ActivateSound;
    public GUISkin Skin;

    private bool AreCreditsDrawn = false;

    void Start()
    {
        renderer.material.SetTexture("_MainTex", DefaultTexture);
    }

    void OnGUI()
    {
        GUI.skin = Skin;
        if (AreCreditsDrawn)
        {
            GUI.Box(new Rect(Screen.width / 2 - 250, Screen.height / 2 - 250, 500, 500), "");
        }
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
        if (AreCreditsDrawn)
        {
            AreCreditsDrawn = false;
        }
        else
        {
            AreCreditsDrawn = true;
        }
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
}

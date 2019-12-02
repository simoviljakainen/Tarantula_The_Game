using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public List<AudioClip> audioClips;
    public static SoundController sc;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        sc = this;
    }

    public void PlaySound(string audioclip, Vector3 position, bool twoD)
    {
        foreach(AudioClip ac in audioClips)
        {
            if(ac.name == audioclip)
            {
                if (twoD)
                {
                    AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                    audioSource.spatialBlend = 0f;
                    audioSource.clip = ac;
                    audioSource.Play();
                }
                else
                {
                    AudioSource.PlayClipAtPoint(ac, position);
                }

            }
        }
    }
}

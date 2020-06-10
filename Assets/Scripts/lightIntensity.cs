using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class lightIntensity : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    public Light lightThing;

    public PostProcessProfile profile;

    public float[] clipSampleData = new float[1024];
    public float clipLoudness = 0f;
    public int sizeFactor;
    public int bloomMultiplier;
    public int minVal, maxVal;

    Bloom bloom;

    private void Start()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    void Update()
    {
        audioSource.clip.GetData(clipSampleData, audioSource.timeSamples);
        clipLoudness = 0f;

        foreach (var samples in clipSampleData)
        {
            clipLoudness += Mathf.Abs(samples);
        }

        clipLoudness /= 1024;

        clipLoudness *= sizeFactor;

        clipLoudness = Mathf.Clamp(clipLoudness, minVal, maxVal);

        lightThing.intensity = clipLoudness * 1.5f;
        profile.TryGetSettings(out bloom);
        bloom.intensity.value = clipLoudness * bloomMultiplier;
    }
}

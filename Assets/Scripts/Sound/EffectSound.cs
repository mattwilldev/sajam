using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class EffectSound : ScriptableObject
{

    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private float _attenuationRange = 50;
    [SerializeField] private FloatRange _volume = new FloatRange(0.7f, 1);
    [SerializeField] private FloatRange _pitch = new FloatRange(0.9f, 1.1f);
    //
    public AudioSource Play()
    {
        AudioSource audioSource = CreateAudioSource(Vector3.zero);
        audioSource.spatialBlend = 0;
        Destroy(audioSource.gameObject, audioSource.clip.length / audioSource.pitch + 0.2f);
        return audioSource;
    }
    public AudioSource Play(Vector3 position)
    { 
        AudioSource audioSource =  CreateAudioSource(position);
        Destroy(audioSource.gameObject, audioSource.clip.length / audioSource.pitch + 0.2f);
        return audioSource;
    }

    protected AudioSource CreateAudioSource(Vector3 position)
    {
        GameObject gO = new GameObject();
        gO.transform.position = position; 
        AudioSource audioSource = gO.AddComponent<AudioSource>();
        audioSource.clip = _clips[Random.Range(0, _clips.Length)];
        gO.name = "OneShotAudio " + audioSource.clip.name;
        audioSource.spatialBlend = 1;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.minDistance = _attenuationRange * 0.15f;
        audioSource.maxDistance = _attenuationRange;
        audioSource.volume = _volume.ChooseRandom();
        audioSource.pitch = _pitch.ChooseRandom();
        audioSource.Play();
        return audioSource;
    }

    
}

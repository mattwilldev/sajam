using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ImpactSound : EffectSound
{
    [SerializeField] private float _minVelocity = 3;
    [SerializeField] private float _maxVelocity = 8;
    [SerializeField] private FloatRange _pitchVelocityMultiplier = new FloatRange(0.7f, 1.1f);
    [SerializeField] private FloatRange _volumeVelocityMultiplier = new FloatRange(0.03f, 1);
    [SerializeField] private float _minRepeatInterval = 0.01f;
    // 
    private float _lastPlayedTime = -100;

    public AudioSource Play(Vector3 position, float impactVelocity)
    {
        if (_lastPlayedTime > Time.time) _lastPlayedTime = 0;
        //
        if (impactVelocity > _minVelocity && Time.time - _lastPlayedTime > _minRepeatInterval)
        {
            AudioSource audioSource = CreateAudioSource(position);
            //
            // ************  MULTIPLIES THE PITCH AND VOLUME BY THE IMPACT VALUE ****
            //
            float velocityM = Mathf.Clamp01((impactVelocity - _minVelocity) / (_maxVelocity - _minVelocity));
            velocityM *= velocityM; // ***** MAKE SOFTER SOUNDS MORE LIKELY **
            //
            audioSource.pitch *= _pitchVelocityMultiplier.GetValue(velocityM);
            audioSource.volume *= _volumeVelocityMultiplier.GetValue(velocityM);
            //
            Destroy(audioSource.gameObject, audioSource.clip.length / audioSource.pitch + 0.2f);
            //
            _lastPlayedTime = Time.time;
            // 
            return audioSource;
        }
        else
        {
            return null;
        }
    }
    
}

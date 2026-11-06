using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static _AudioManager;

public class _AudioManager : MonoBehaviour
{
    public static _AudioManager Instance { get; private set; }
    [SerializeField] List<AudioClip> _AudioList;
    AudioSource _audioSourceSelf;

    AudioSource _audioSource3D;
    AudioSource _ambience;
    AudioSource _music;

    [SerializeField] float defaultVol;
    [SerializeField] float defaultPitch;
    [SerializeField] float defaultPan;
    bool fading;
    AudioSource _tempAudioSource;
    float _timeToFade;
    float _currentTime;

    public Dictionary<Clip, AudioClip> _AudioDictionary = new Dictionary<Clip, AudioClip>();
    public enum PlayType
    {
        Default,
        VolPitch,
        Pos,
        PosVolPitch

    }

    public enum Clip
    {
        ElectricMosquito,
        ElectricMosquito2,
        ElectricMosquito3,
        ElectricMosquito4,
        Piniorb,
        Piniorb2,
        Piniorb3,
        Piniorb4,
        RegularDude,
        RegularDude2,
        RegularDude3,
        RegularDude4,
        RegularDude5,
        RegularDude6,
        marimbaMusic_ChromeMusicLab,
        ambientMusic_BeepBox,
        ice_attack_FINAL,
        punch_hit_FINAL

    }

    private void Awake()
    {
        if (Instance != null && Instance != this) //instance makes a singleton, only one single instance of the script is needed so it looks for any other existing
        {                                           //instances and destroys itself if there are more than one
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);          //carries object through different scenes

        _audioSourceSelf = GetComponent<AudioSource>(); //audio source on self for 2D audio
        _audioSource3D = transform.GetChild(0).GetComponent<AudioSource>(); //audio source on other object for spatial 3D sound
        _ambience = transform.GetChild(1).GetComponent<AudioSource>(); //audio source on other object for ambience
        _music = transform.GetChild(2).GetComponent<AudioSource>(); //audio source on other object for music

        foreach (Clip type in (Clip[])Enum.GetValues(typeof(Clip))) //looks through the manually filled enum "Clip" and correlates each value's name to the 
        {                                                           //clip with the same name, then adds to a dictionary for future lookup
            foreach (AudioClip clip in _AudioList)
            {
                if (type.ToString().Equals(clip.name))
                {
                    _AudioDictionary.TryAdd(type, clip);
                    break;
                }
            }
        }

        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fading)
        {
            if (_currentTime <= _timeToFade)
            {
                _currentTime += Time.deltaTime;
                _tempAudioSource.volume = Mathf.Lerp(0,_timeToFade,_currentTime/_timeToFade);
            }
            else
            {
                fading = false;
                _currentTime = 0;
                _timeToFade = 0;
                _tempAudioSource = null;
            }
        }
    }

    public void CallAudio(Clip type) //calls audio clip and plays according to enum type
    {
        ResetVars();
        PlayAudio(_AudioDictionary[type],defaultVol,defaultPitch,defaultPan);    
    }

    public void CallAudio(string clipName, PlayType type, float vol, float pitch, float pan, Transform pos) // pan = left -> right panning
    {
        ResetVars();
        foreach (var clip in _AudioList)
        {
            if (clip.name.Equals(clipName))
            {
                switch (type)
                {
                    case PlayType.Default:
                    {                                                     //Searches for clip by name, filtered by type of play (3D Spatial, 2D, using default
                        PlayAudio(clip);                                  //or custom values for volume/pitch/pan/position
                        break;                                            
                    }
                    case PlayType.VolPitch:
                    {
                        PlayAudio(clip, vol, pitch, pan);
                        break;
                    }
                    case PlayType.Pos:
                    {
                        PlayAudioAtPosition(clip, pos);
                        break;
                    }
                    case PlayType.PosVolPitch:
                    {
                            PlayAudioAtPosition(clip,pos,vol,pitch);
                            break;
                    }
                }
            }
        }
    }

    public void PlayAudio(AudioClip clip) //callable function by buttons simply attach an audio clip
    {
        ResetVars();
        _audioSourceSelf.PlayOneShot(clip);

    }

    public void PlayAudio(AudioClip clip, float vol, float pitch, float pan) //play audio override for multiple parameters
    {
        ResetVars();
        _audioSourceSelf.volume = vol;
        _audioSourceSelf.pitch = pitch;
        _audioSourceSelf.panStereo = pan;
        _audioSourceSelf.PlayOneShot(clip);

    }

    public void PlayAudioAtPosition(AudioClip clip, Transform pos) //play audio 3D space default
    {
        ResetVars();
        _audioSource3D.transform.position = pos.position;
        _audioSource3D.PlayOneShot(clip);

    }

    public void PlayAudioAtPosition(AudioClip clip, Transform pos, float vol, float pitch) //play audio 3D space override with custom values
    {
        ResetVars();
        _audioSource3D.transform.position = pos.position;
        _audioSource3D.volume = vol;
        _audioSource3D.pitch = pitch;
        _audioSource3D.PlayOneShot(clip);

    }

    public void PlayAudioAtPosition(Clip type, Transform pos, float vol)
    {
        ResetVars();
        _audioSource3D.transform.position = pos.position;
        _audioSource3D.volume = vol;
        _audioSource3D.clip = _AudioDictionary[type];
        _audioSource3D.Play();
    }

    void ResetVars()        //resets variables to not carry over previous custom settings
    {
        _audioSource3D.volume = defaultVol;
        _audioSource3D.pitch = defaultPitch;
        _audioSource3D.panStereo = defaultPan;
        _audioSource3D.transform.position = new Vector3(0, 0, 0);
        _audioSourceSelf.volume = defaultVol;
        _audioSourceSelf.pitch = defaultPitch;
        _audioSourceSelf.panStereo = defaultPan;
    }

    public void TriggerAmbience(Clip type)
    {
        _ambience.clip = _AudioDictionary[type];
        _ambience.Play();
    }

    public void TriggerAmbience()
    {
        if (_ambience.isPlaying)
        {
            _ambience.Stop();
        }
        else
        {
            _ambience.Play();
        }
    }
    public void TriggerAmbience(bool condition)
    {
        if (!condition)
        {
            _ambience.Stop();
        }
        else
        {
            _ambience.Play();
        }
    }

    public void PlayMusic(Clip type)
    {
        _music.clip = _AudioDictionary[type];
        _music.Play();
    }

    public void TriggerMusic()
    {
        if (_music.isPlaying)
        {
            _music.Stop();
        }
        else
        {
            _music.Play();
        }

    }
    public void TriggerMusic(bool condition)
    {
        if (!condition)
        {
            _music.Stop();
        }
        else
        {
            _music.Play();
        }

    }

    public void FadeMusic(Clip type, float timeToFade, int condition)
    {
        fading = true;
        _music.clip = _AudioDictionary[type];
        _music.Play();
        _tempAudioSource = _music;
        _timeToFade = condition * timeToFade;
        _tempAudioSource.volume = 0;
    }
    public void FadeAmbience(Clip type, float timeToFade, int condition)
    {
        fading = true;
        _ambience.clip = _AudioDictionary[type];
        _ambience.Play();
        _tempAudioSource = _ambience;
        _timeToFade = condition * timeToFade;
        _tempAudioSource.volume = 0;
        
    }

}

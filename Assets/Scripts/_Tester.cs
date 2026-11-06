using UnityEngine;
using static _AudioManager;

public class _Tester : MonoBehaviour
{
    [SerializeField] Transform[] _transformArray = new Transform[4];
    [SerializeField] AudioClip[] _testClips = new AudioClip[4];

    //for testing

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _AudioManager.Instance.PlayAudioAtPosition(_testClips[0], _transformArray[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _AudioManager.Instance.PlayAudioAtPosition(_testClips[1], _transformArray[1], 0.4f, 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _AudioManager.Instance.PlayAudioAtPosition(_testClips[2], _transformArray[2]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _AudioManager.Instance.PlayAudioAtPosition(_testClips[3], _transformArray[3], 1, 2f);
        }
        if (Input.GetKeyDown (KeyCode.Alpha5))
        {
            _AudioManager.Instance.PlayMusic(Clip.marimbaMusic_ChromeMusicLab);
            _AudioManager.Instance.TriggerAmbience(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            _AudioManager.Instance.TriggerMusic(false);
            _AudioManager.Instance.TriggerAmbience(Clip.ambientMusic_BeepBox);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            _AudioManager.Instance.FadeMusic(Clip.marimbaMusic_ChromeMusicLab, 5,1);
            _AudioManager.Instance.TriggerAmbience(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {

            _AudioManager.Instance.FadeAmbience(Clip.ambientMusic_BeepBox, 5, 1);
            _AudioManager.Instance.TriggerMusic(false);
        }
    }
}

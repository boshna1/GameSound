using System;
using UnityEngine;

public class _MonsterRelay : MonoBehaviour
{
    [SerializeField] _AudioManager.Clip Clip;
    [SerializeField] float vol;
    public void Relay()
    {
        _AudioManager.Instance.PlayAudioAtPosition(Clip,transform,vol);
    }
}

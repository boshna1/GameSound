using UnityEngine;

public class _AudioButton : MonoBehaviour
{
    //script to assign enum according to desired clip
    //allows buttons to reference audio manageer since enums are not allowed in button function parameters
    [SerializeField] public _AudioManager.Clip clip;

    public void Play()
    {
        _AudioManager.Instance.CallAudio(clip);
    }
}

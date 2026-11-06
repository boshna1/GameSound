using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static _AudioManager;

public class _ButtonGenerator : MonoBehaviour
{
    [SerializeField] GameObject _audioButtonPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (_AudioManager.Clip type in (Clip[])Enum.GetValues(typeof(Clip)))                           // makes a list of debugging buttons to test out sounds
        {                                                                                                   //procedural with each startup of editor play or game
            GameObject temp = Instantiate(_audioButtonPrefab, transform.GetChild(0).GetChild(0),false);     //modular and will update with new audio clips and corresponding
            temp.GetComponent<_AudioButton>().clip = type;                                                  //enums added
            temp.transform.GetChild(0).GetComponent<Text>().text = type.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}

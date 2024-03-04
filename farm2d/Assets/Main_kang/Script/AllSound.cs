using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class SetValure : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetLevel(float sliderVal)
    {
        mixer.SetFloat("Master", Mathf.Log10(sliderVal) * 20);
    }
    public void SetLevel2(float sliderVal)
    {
        mixer.SetFloat("BGM", Mathf.Log10(sliderVal) * 20);
    }
    public void SetLevel3(float sliderVal)
    {
        mixer.SetFloat("Talk", Mathf.Log10(sliderVal) * 20);
    }
}
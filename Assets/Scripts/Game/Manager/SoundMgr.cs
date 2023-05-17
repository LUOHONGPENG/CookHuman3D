using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    TooYoung,
    TooOld,
    MoreEdu,
    MoreCareer,
    Married
}

public class SoundMgr : MonoBehaviour
{
    public AudioSource auTooYoung;
    public AudioSource auTooOld;
    public AudioSource auMoreEdu;
    public AudioSource auMoreCareer;
    public AudioSource auMarried;

    public Dictionary<SoundType, AudioSource> dicSoundAudio = new Dictionary<SoundType, AudioSource>();
    public Dictionary<SoundType, float> dicSoundTime = new Dictionary<SoundType, float>();

    public void Init()
    {
        dicSoundAudio.Clear();
        dicSoundAudio.Add(SoundType.TooYoung, auTooYoung);
        dicSoundAudio.Add(SoundType.TooOld, auTooOld);
        dicSoundAudio.Add(SoundType.MoreEdu, auMoreEdu);
        dicSoundAudio.Add(SoundType.MoreCareer, auMoreCareer);
        dicSoundAudio.Add(SoundType.Married, auMarried);

        dicSoundTime.Clear();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    TooYoung,
    TooOld,
    MoreEdu,
    MoreCareer,
    Married,
    NoSpace,
    Study,
    Job,
    Marriage,
    Retire
}

public class SoundMgr : MonoBehaviour
{
    public AudioSource auTooYoung;
    public AudioSource auTooOld;
    public AudioSource auMoreEdu;
    public AudioSource auMoreCareer;
    public AudioSource auMarried;
    public AudioSource auNoSpace;
    public AudioSource auStudy;
    public AudioSource auJob;
    public AudioSource auMarriage;
    public AudioSource auRetire;

    public Dictionary<SoundType, AudioSource> dicSoundAudio = new Dictionary<SoundType, AudioSource>();
    public Dictionary<SoundType, float> dicSoundTime = new Dictionary<SoundType, float>();

    public void OnEnable()
    {
        EventCenter.Instance.AddEventListener("PlaySound", PlaySound);
    }

    public void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener("PlaySound", PlaySound);
    }

    public void Init()
    {
        dicSoundAudio.Clear();
        dicSoundAudio.Add(SoundType.TooYoung, auTooYoung);
        dicSoundAudio.Add(SoundType.TooOld, auTooOld);
        dicSoundAudio.Add(SoundType.MoreEdu, auMoreEdu);
        dicSoundAudio.Add(SoundType.MoreCareer, auMoreCareer);
        dicSoundAudio.Add(SoundType.Married, auMarried);
        dicSoundAudio.Add(SoundType.NoSpace, auNoSpace);
        dicSoundAudio.Add(SoundType.Study, auStudy);
        dicSoundAudio.Add(SoundType.Job, auJob);
        dicSoundAudio.Add(SoundType.Marriage, auMarriage);
        dicSoundAudio.Add(SoundType.Retire, auRetire);

        dicSoundTime.Clear(); dicSoundTime.Add(SoundType.Marriage, 0.7f);
    }

    public void PlaySound(object arg0)
    {
        Debug.Log(111);
        SoundType soundType = (SoundType)arg0;

        if (dicSoundAudio.ContainsKey(soundType))
        {
            AudioSource targetSound = dicSoundAudio[soundType];

            float playTime = 0.5f;
            if (dicSoundTime.ContainsKey(soundType))
            {
                playTime = dicSoundTime[soundType];
            }
            targetSound.time = playTime;
            targetSound.Play();
        }
    }
}

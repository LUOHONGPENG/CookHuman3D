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
    Retire,
    Gay,
    Lesbian,
    Hurry,
    Please,
    MagicBelly,
    ParentEffort
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

    public AudioSource auGay;
    public AudioSource auLesbian;
    public AudioSource auHurry;
    public AudioSource auPlease;
    public AudioSource auMagicBelly;
    public AudioSource auParentEffort;


    public Dictionary<SoundType, AudioSource> dicSoundAudio = new Dictionary<SoundType, AudioSource>();
    public Dictionary<SoundType, float> dicSoundTime = new Dictionary<SoundType, float>();

    [Header("Test")]
    public SoundType testSoundType;

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

        dicSoundAudio.Add(SoundType.Gay, auGay);
        dicSoundAudio.Add(SoundType.Lesbian, auLesbian);
        dicSoundAudio.Add(SoundType.Hurry, auHurry);
        dicSoundAudio.Add(SoundType.Please, auPlease);
        dicSoundAudio.Add(SoundType.MagicBelly, auMagicBelly);
        dicSoundAudio.Add(SoundType.ParentEffort, auParentEffort);

        dicSoundTime.Clear();

        dicSoundTime.Add(SoundType.TooYoung, 0.5f);
        dicSoundTime.Add(SoundType.TooOld, 0.5f);
        dicSoundTime.Add(SoundType.MoreEdu, 0.3f);
        dicSoundTime.Add(SoundType.MoreCareer, 0.4f);
        dicSoundTime.Add(SoundType.Married, 0.5f);
        dicSoundTime.Add(SoundType.NoSpace, 0.75f);

        dicSoundTime.Add(SoundType.Study, 0.7f);
        dicSoundTime.Add(SoundType.Job, 0.6f);
        dicSoundTime.Add(SoundType.Marriage, 0.8f);
        dicSoundTime.Add(SoundType.Retire, 0.5f);

        dicSoundTime.Add(SoundType.Gay, 1.1f);
        dicSoundTime.Add(SoundType.Lesbian, 0.8f);
        dicSoundTime.Add(SoundType.Hurry, 0.5f);
        dicSoundTime.Add(SoundType.Please, 0.6f);
        dicSoundTime.Add(SoundType.MagicBelly, 0.6f);
        dicSoundTime.Add(SoundType.ParentEffort, 1.6f);

    }

    public void PlaySound(object arg0)
    {
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

    public void PlaySoundTime(SoundType soundType,float playtime)
    {
        if (dicSoundAudio.ContainsKey(soundType))
        {
            AudioSource targetSound = dicSoundAudio[soundType];

            float playTime = playtime;
            targetSound.time = playTime;
            targetSound.Play();
        }
    }
}

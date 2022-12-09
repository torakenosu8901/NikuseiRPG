using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class SoundManager : SingletonClass<SoundManager>
{
    private const string BGM_PATH = "Audio/BGM";
    private const string SE_PATH = "Audio/SE";
    private const string VC_PATH = "Audio/VC";
    private const string SOUND_OBJECT_NAME = "SoundManager";
    private const int BGM_SOURCE_NUM = 1;
    private const int SE_SOURCE_NUM = 5;
    private const int VC_SOURCE_NUM = 5;
    private const float FADE_OUT_SECONDO = 0.5f;
    private const float BGM_VOLUME = 0.5f;
    private const float SE_VOLUME = 0.3f;
    private const float VC_VOLUME = 0.3f;

    private bool isFadeOut = false;
    private float fadeDeltaTime = 0f;
    private int nextSESourceNum = 0;
    private int nextVCSourceNum = 0;
    private BGMLabel currentBGM = BGMLabel.BGM1;
    private BGMLabel nextBGM = BGMLabel.BGM1;

    // BGMは一つづつ鳴るが、SEは複数同時に鳴ることがある
    [SerializeField] private AudioSource bgmSource;
    private List<AudioSource> seSourceList;
    private List<AudioSource> vcSourceList;
    [SerializeField] private SoundData soundData;
    [SerializeField] private SEData SEData;
    [SerializeField] private VCData VCData;








    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < SE_SOURCE_NUM + BGM_SOURCE_NUM; i++)
        {
            gameObject.AddComponent<AudioSource>();
        }

        IEnumerable<AudioSource> audioSources = GetComponents<AudioSource>().Select(a => { a.playOnAwake = false; a.volume = BGM_VOLUME; a.loop = true; return a; });
        bgmSource = audioSources.First();
        seSourceList = audioSources.Skip(BGM_SOURCE_NUM).ToList();
        seSourceList.ForEach(a => { a.volume = SE_VOLUME; a.loop = false; });
        vcSourceList = audioSources.Skip(BGM_SOURCE_NUM).ToList();
        vcSourceList.ForEach(a => { a.volume = VC_VOLUME; a.loop = false; });

        // bgmClipDic = (Resources.LoadAll(BGM_PATH) as Object[]).ToDictionary(bgm => bgm.name, bgm => (AudioClip)bgm);
        // seClipDic = (Resources.LoadAll(SE_PATH) as Object[]).ToDictionary(se => se.name, se => (AudioClip)se);
    }


    /// <summary>
    /// 指定したファイル名のSEを流す。第二引数のdelayに指定した時間だけ再生までの間隔を空ける
    /// </summary>
    /// /// <param name="seLabel"></param>
    /// /// <param name="delay"></param>
    public void PlaySE(SELabel seLabel, float delay = 0.0f) => StartCoroutine(DelayPlaySE(seLabel, delay));

    private IEnumerator DelayPlaySE(SELabel seLabel, float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioSource se = seSourceList[nextSESourceNum];
        se.PlayOneShot(SEData.SEDataPairs[(int)seLabel].audioClip);
        nextSESourceNum = (++nextSESourceNum < SE_SOURCE_NUM) ? nextSESourceNum : 0;
        //bgmSource.clip = soundData.bgmDataPairs[(int)seLabel].audioClip;
        
        
    }

    /// <summary>
    /// 指定したファイル名のVCを流す。第二引数のdelayに指定した時間だけ再生までの間隔を空ける
    /// </summary>
    /// /// <param name="vcLabel"></param>
    /// /// <param name="delay"></param>
    public void PlayVC(VCLabel vcLabel, float delay = 0.0f) => StartCoroutine(DelayPlayVC(vcLabel, delay));

    private IEnumerator DelayPlayVC(VCLabel vcLabel, float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioSource vc = vcSourceList[nextVCSourceNum];
        vc.PlayOneShot(VCData.VCDataPairs[(int)vcLabel].audioClip);
        nextVCSourceNum = (++nextVCSourceNum < VC_SOURCE_NUM) ? nextVCSourceNum : 0;
        //bgmSource.clip = soundData.bgmDataPairs[(int)seLabel].audioClip;


    }

    /// <summary>
    /// 指定したBGMを流す。すでに流れている場合はNextに予約し、流れているBGMをフェードアウトさせる
    /// </summary>
    /// <param name="bgmLabel"></param>
    public void PlayBGM(BGMLabel bgmLabel)
    {
        if (!bgmSource.isPlaying)
        {
            currentBGM = bgmLabel;
            nextBGM = BGMLabel.BGM1;

            bgmSource.clip = soundData.bgmDataPairs[(int)bgmLabel].audioClip;

            bgmSource.Play();
        }
        else if (currentBGM != bgmLabel)
        {
            isFadeOut = true;
            nextBGM = bgmLabel;
            fadeDeltaTime = 0f;
        }
    }


    /// <summary>
    /// BGMを止める
    /// </summary>
    public void StopSound()
    {
        bgmSource.Stop();
        seSourceList.ForEach(a => { a.Stop(); });
    }


    private void Update()
    {
        if (isFadeOut)
        {
            fadeDeltaTime += Time.deltaTime;
            bgmSource.volume = (1.0f - fadeDeltaTime / FADE_OUT_SECONDO) * BGM_VOLUME;

            if (fadeDeltaTime >= FADE_OUT_SECONDO)
            {
                isFadeOut = false;
                bgmSource.Stop();
            }
        }
        else if (nextBGM != BGMLabel.BGM1)
        {
            bgmSource.volume = BGM_VOLUME;
            PlayBGM(nextBGM);
        }
    }
}
    

    public enum BGMLabel
    {
        BGM1,
        BGM2,
        BGM3

    }

    public enum SELabel
    {
      SE1,
      SE2,
      SE3
    }

public enum VCLabel
{
    VC1,
    VC2,
    VC3,
    VC4,
    VC5,
    VC6,
    VC7,
    VC8,
    VC9,
    VC10
}


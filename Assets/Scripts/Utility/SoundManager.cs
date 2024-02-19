using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    //오디오 소스
    [SerializeField] private AudioSource BGMPlayer;
    [SerializeField] private AudioSource FXPlayer;

    //오디오 클립 배열
    public AudioClip[] BGMClips;
    public AudioClip[] meowClips;

    public static SoundManager instance;

    private void Awake()
    {
        #region singleton

        if (instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoad;
        }
        else
            Destroy(gameObject);

        //씬이 전환되어도 없어지지 않음
        DontDestroyOnLoad(gameObject);

        #endregion singleton
    }

    //씬 로딩 시 해당 씬 이름과 같은 이름의 BGM 재생
    private void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        for (int i = 0; i < BGMClips.Length; i++)
        {
            if (scene.name == BGMClips[i].name)
            {
                PlayBGM(BGMClips[i]);
            }
        }
    }

    //BGM 재생
    public void PlayBGM(AudioClip clip)
    {
        BGMPlayer.clip = clip;
        BGMPlayer.Play();
    }


    //고양이 효과음 재생
    public void PlayMeow()
    {
        if (FXPlayer != null)
        {
            FXPlayer.clip = meowClips[Random.Range(0, 6)];
            FXPlayer.Play();
        }
    }

    //클리어 효과음
    public void ClearSound()
    {
        FXPlayer.clip = meowClips[6];
        FXPlayer.Play();
    }
}

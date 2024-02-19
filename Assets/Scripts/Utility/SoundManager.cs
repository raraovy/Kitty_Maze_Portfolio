using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    //����� �ҽ�
    [SerializeField] private AudioSource BGMPlayer;
    [SerializeField] private AudioSource FXPlayer;

    //����� Ŭ�� �迭
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

        //���� ��ȯ�Ǿ �������� ����
        DontDestroyOnLoad(gameObject);

        #endregion singleton
    }

    //�� �ε� �� �ش� �� �̸��� ���� �̸��� BGM ���
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

    //BGM ���
    public void PlayBGM(AudioClip clip)
    {
        BGMPlayer.clip = clip;
        BGMPlayer.Play();
    }


    //����� ȿ���� ���
    public void PlayMeow()
    {
        if (FXPlayer != null)
        {
            FXPlayer.clip = meowClips[Random.Range(0, 6)];
            FXPlayer.Play();
        }
    }

    //Ŭ���� ȿ����
    public void ClearSound()
    {
        FXPlayer.clip = meowClips[6];
        FXPlayer.Play();
    }
}

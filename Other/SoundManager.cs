using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // adapted from Small Hedge Games youtube video - PLEASE use a Unity SOUND MANAGER! - Full Tutorial

    public static SoundManager Instance;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<Define.SoundData> soundList;
    public Dictionary<Define.SoundType, AudioClip> SoundDictionary { get; private set; }

    private void Awake()
    {
        // singleton code
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;


        // add sounds to dictionary
        SoundDictionary = new Dictionary<Define.SoundType, AudioClip>();
        foreach (Define.SoundData soundData in soundList)
        {
            SoundDictionary.Add(soundData.soundType, soundData.AudioClip);
        }
    }

    private void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }

    public static void PlaySound(Define.SoundType soundType, float volume = 1)
    {
        Instance.audioSource.PlayOneShot(Instance.SoundDictionary[soundType] , volume);
    }

    public static void PlaySound(AudioClip audio, float volume = 1)
    {
        Instance.audioSource.PlayOneShot(audio, volume);
    }

}

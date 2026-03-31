using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip defenseSound;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip healSound;
    [SerializeField] private AudioClip itemPickupSound;
    [SerializeField] private float masterVolume = 0.8f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        Debug.Log("✅ SoundManager inicializado");
    }

    public void PlayAttackSound()
    {
        PlaySound(attackSound, 0.6f);
    }

    public void PlayDefenseSound()
    {
        PlaySound(defenseSound, 0.5f);
    }

    public void PlayDamageSound()
    {
        PlaySound(damageSound, 0.7f);
    }

    public void PlayHealSound()
    {
        PlaySound(healSound, 0.6f);
    }

    public void PlayItemPickupSound()
    {
        PlaySound(itemPickupSound, 0.5f);
    }

    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        if (clip == null)
        {
            Debug.LogWarning("⚠️ AudioClip es null");
            return;
        }

        audioSource.PlayOneShot(clip, volume * masterVolume);
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        audioSource.volume = masterVolume;
        Debug.Log($"🔊 Volumen maestro: {masterVolume * 100}%");
    }

    public float GetMasterVolume() => masterVolume;
    public static SoundManager GetInstance() => instance;
}
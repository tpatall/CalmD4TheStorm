using UnityEngine;

public class MusicPlayer : PersistentSingleton<MusicPlayer>
{
    [SerializeField] private AudioClip titleMusic;
    [SerializeField] private AudioClip mapMusic;
    [SerializeField] private AudioClip battleMusic;
    [SerializeField] private AudioClip bossMusic;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusicUI(AudioClip clip) {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayTitleMusic() {
        audioSource.clip = titleMusic;
        audioSource.Play();
    }

    public void PlayMapMusic() {
        audioSource.clip = mapMusic;
        audioSource.Play();
    }

    public void PlayBattleMusic() {
        audioSource.clip = battleMusic;
        audioSource.Play();
    }

    public void PlayBossMusic() {
        audioSource.clip = bossMusic;
        audioSource.Play();
    }

    /// <summary>
    ///     Play music once the player died in battle.
    /// </summary>
    public void PlayGameOverMusic() {
        audioSource.clip = bossMusic;
        audioSource.Play();
    }
}

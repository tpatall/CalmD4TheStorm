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
        // Already playing this music.
        if (audioSource.clip == clip)
            return;

        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayTitleMusic() {
        PlayMusicUI(titleMusic);
    }

    public void PlayMapMusic() {
        PlayMusicUI(mapMusic);
    }

    public void PlayBattleMusic() {
        PlayMusicUI(battleMusic);
    }

    public void PlayBossMusic() {
        PlayMusicUI(bossMusic);
    }

    /// <summary>
    ///     Play music once the player died in battle.
    /// </summary>
    public void PlayGameOverMusic() {
        PlayMusicUI(bossMusic);
    }
}

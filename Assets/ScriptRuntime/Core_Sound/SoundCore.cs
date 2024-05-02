using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.AddressableAssets;
public class SoundCore {

    public AudioSource bgmPlayer;
    public AudioSource bubbleBreak;
    public AudioSource bubbleShoot;
    public AudioSource prefab;


    public void Load() {
        prefab = Addressables.LoadAssetAsync<GameObject>("AudioSource").WaitForCompletion().GetComponent<AudioSource>();

        GameObject sfx = new GameObject("SFX");
        bgmPlayer = GameObject.Instantiate(prefab, sfx.transform);
        bubbleBreak = GameObject.Instantiate(prefab, sfx.transform);
        bubbleShoot = GameObject.Instantiate(prefab, sfx.transform);
    }

    public void BgmPlay(AudioClip clip) {
        bgmPlayer.loop = true;
        if (!bgmPlayer.isPlaying) {
            bgmPlayer.clip = clip;
            bgmPlayer.Play();
        }
    }

    public void BubbleShootPlay(AudioClip clip) {
        if (!bubbleShoot.isPlaying) {
            bubbleShoot.clip = clip;
            bubbleShoot.Play();
        }
    }
}
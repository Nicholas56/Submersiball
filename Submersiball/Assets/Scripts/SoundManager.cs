using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource source;
    AudioSource sfx;

    [SerializeField] AudioClip subEngine;
    bool engine;
    [SerializeField] [Range(0, 1)] float subEngineVolume = 1;
    [SerializeField] AudioClip subBoost;
    [SerializeField] [Range(0, 1)] float subBoostVolume = 1;
    [SerializeField] AudioClip explosion;
    [SerializeField] [Range(0, 1)] float explosionVolume = 1;
    [SerializeField] AudioClip mineExplosion;
    [SerializeField] [Range(0, 1)] float mineExplodeVolume = 1;
    [SerializeField] AudioClip goalScore;
    [SerializeField] [Range(0, 1)] float scoreGoalVolume = 1;
    [SerializeField] AudioClip buttonPress;
    [SerializeField] [Range(0, 1)] float buttonPressVolume = 1;
    [SerializeField] List<AudioClip> tracks;
    int currentTrack = 0;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        sfx = transform.GetChild(0).GetComponent<AudioSource>();
        sfx.clip = subEngine;
    }
    private void Start()
    {
        GameEvents.current.onPressButton += PlayButtonPress;
        GameEvents.current.onAccelerate += PlayEngine;
        GameEvents.current.onBoost += PlayBoost;
        GameEvents.current.onExplode += PlayExplosion;
        GameEvents.current.onMineExplode += PlayMineExplode;
        GameEvents.current.onScoreGoal += PlayGoalScore;
    }
    private void OnDestroy()
    {
        GameEvents.current.onPressButton -= PlayButtonPress;
        GameEvents.current.onAccelerate -= PlayEngine;
        GameEvents.current.onBoost -= PlayBoost;
        GameEvents.current.onExplode -= PlayExplosion;
        GameEvents.current.onMineExplode -= PlayMineExplode;
        GameEvents.current.onScoreGoal -= PlayGoalScore;
    }

    void PlayEngine(bool accel)
    {
        if (sfx.clip != null)
        {
            engine = accel;
        }
    }
    void PlayBoost() {if(subBoost!=null) sfx.PlayOneShot(subBoost, subBoostVolume); }
    void PlayExplosion() {if(explosion!=null) sfx.PlayOneShot(explosion, explosionVolume); }
    void PlayMineExplode() { if (mineExplosion != null) sfx.PlayOneShot(mineExplosion, mineExplodeVolume); }
    void PlayGoalScore() {if(goalScore!=null) sfx.PlayOneShot(goalScore, scoreGoalVolume); }
    void PlayButtonPress() { if (buttonPress != null) sfx.PlayOneShot(buttonPress, buttonPressVolume); }
    private void Update()
    {
        if (source.clip != null)
        {
            if (source.isPlaying == false) { PlayNextTrack(); }
        }
        if (engine) { sfx.volume = Mathf.Lerp(sfx.volume, subEngineVolume, Time.deltaTime); }
        else { sfx.volume = Mathf.Lerp(sfx.volume, 0, Time.deltaTime); }
    }


    void PlayNextTrack()
    {
        source.clip = tracks[currentTrack];
        source.Play();
        currentTrack++;
        if (currentTrack >= tracks.Count) { currentTrack = 0; }
    }
}

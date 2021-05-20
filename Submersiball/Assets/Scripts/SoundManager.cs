using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource source;
    AudioSource sfx;

    [SerializeField] AudioClip subEngine;
    [SerializeField] AudioClip subBoost;
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip mineExplosion;
    [SerializeField] AudioClip goalScore;
    [SerializeField] List<AudioClip> tracks;
    int currentTrack = 0;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        sfx = transform.GetChild(0).GetComponent<AudioSource>();
    }
    private void Start()
    {
        GameEvents.current.onAccelerate += PlayEngine;
        GameEvents.current.onBoost += PlayBoost;
        GameEvents.current.onExplode += PlayExplosion;
        GameEvents.current.onMineExplode += PlayMineExplode;
        GameEvents.current.onScoreGoal += PlayGoalScore;
    }
    private void OnDestroy()
    {
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
            if (accel) 
            {
                sfx.Play(); 
            }
        else 
            {
                sfx.Stop(); 
            } 
        }
    }
    void PlayBoost() {if(subBoost!=null) sfx.PlayOneShot(subBoost, 1); }
    void PlayExplosion() {if(explosion!=null) sfx.PlayOneShot(explosion, 1); }
    void PlayMineExplode() { if (mineExplosion != null) sfx.PlayOneShot(mineExplosion, 1); }
    void PlayGoalScore() {if(goalScore!=null) sfx.PlayOneShot(goalScore, 1); }
    private void Update()
    {
        if (source.clip != null)
        {
            if (source.isPlaying == false) { PlayNextTrack(); }
        }
    }


    void PlayNextTrack()
    {
        source.clip = tracks[currentTrack];
        source.Play();
        currentTrack++;
        if (currentTrack >= tracks.Count) { currentTrack = 0; }
    }
}

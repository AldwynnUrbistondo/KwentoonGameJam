using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    PlayerProjectile,
    RockProjectile,
    FreezeProjectile,
    EnemyHit,
    EnemyDeath,
    BaseHit,
    ButtonBuy,
    ButtonClick,
    TowerPlace,
    TowerDestroy
}

public class AudioManager : MonoBehaviour
{
    public AudioSource[] playerProjectileSound;
    public AudioSource[] rockProjecileSound;
    public AudioSource[] freezeProjectileSound;
    public AudioSource[] enemyHitSound;
    public AudioSource[] enemyDeathSound;
    public AudioSource[] baseHitSound;

    public AudioSource buttonBuySound;
    public AudioSource buttonClickSound;
    public AudioSource towerPlaceSound;
    public AudioSource towerDestroySound;

    public void PlaySound(SoundType soundType)
    {
        switch (soundType)
        {
            // = = = = = = = = Multiple Channel = = = = = = = = 
            case SoundType.PlayerProjectile:
                AudioPlay(playerProjectileSound);
                break;

            case SoundType.RockProjectile:
                AudioPlay(rockProjecileSound);
                break;

            case SoundType.FreezeProjectile:
                AudioPlay(freezeProjectileSound);
                break;

            case SoundType.EnemyHit:
                AudioPlay(enemyHitSound);
                break;

            case SoundType.EnemyDeath:
                AudioPlay(enemyDeathSound);
                break;

            case SoundType.BaseHit:
                AudioPlay(baseHitSound);
                break;

            // = = = = = = = = Single Channel = = = = = = = = 
            case SoundType.ButtonBuy:
                buttonBuySound.Play();
                break;

            case SoundType.ButtonClick:
                buttonClickSound.Play();
                break;

            case SoundType.TowerPlace:
                towerPlaceSound.Play();
                break;

            case SoundType.TowerDestroy:
                towerDestroySound.Play();
                break;
        }
    }

    void AudioPlay(AudioSource[] audioSources)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (!audioSources[i].isPlaying)
            {
                audioSources[i].Play();
                return;
            }
        }

        audioSources[0].Play();

    }
}



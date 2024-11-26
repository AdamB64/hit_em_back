using UnityEngine;

public class GameManagerAudio : MonoBehaviour
{
    // References to the AudioSource components
    public AudioSource healAudioSource;
    public AudioSource equipAudioSource;
    public AudioSource damageAudioSource;
    public AudioSource WalkingAudio;
    public AudioSource AttackAudio;
    public AudioSource unequipAudioSource;

    // Public methods to play each sound
    public void PlayHealSound()
    {
        if (healAudioSource != null)
        {
            healAudioSource.Play();
        }
        else
        {
            Debug.LogError("Heal AudioSource not assigned!");
        }
    }

    public void PlayEquipSound()
    {
        if (equipAudioSource != null)
        {
            equipAudioSource.Play();
        }
        else
        {
            Debug.LogError("Buff AudioSource not assigned!");
        }
    }

    public void PlayDamageSound()
    {
        if (damageAudioSource != null)
        {
            damageAudioSource.Play();
        }
        else
        {
            Debug.LogError("Damage AudioSource not assigned!");
        }
    }

    public void WalkingAudioSound()
    {
        if (WalkingAudio != null)
        {
            WalkingAudio.Play();
        }
        else
        {
            Debug.LogError("Damage AudioSource not assigned!");
        }
    }

    public void AttackAudioSound() {
        if (AttackAudio != null) { 
            AttackAudio.Play();
        }
        else
        {
            Debug.LogError("Attack Audiosource not assigneed");
        }
    }
    public void UnequipAudioSound() {
        if (unequipAudioSource != null)
        {
            unequipAudioSource.Stop();
        }
        else
        {
            Debug.LogError("Buff AudioSource not assigned!");
        }
    }
}

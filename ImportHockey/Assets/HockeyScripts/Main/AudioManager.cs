using UnityEngine;

// Thanks to this script, the sounds for the game will be activated.
namespace HockeyScripts.Main
{
    public class AudioManager : MonoBehaviour
    {
        public AudioClip puckCollision;
        public AudioClip goal;
        public AudioClip lostGame;
        public AudioClip wonGame;
        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayPuckCollision()
        {
            audioSource.PlayOneShot(goal);
        }

        public void PlayGoal()
        {
            audioSource.PlayOneShot(goal);

        }
    
        public void PlayLostGame()
        {
            audioSource.PlayOneShot(lostGame);

        }

        public void PlayWonGame()
        {
            audioSource.PlayOneShot(wonGame);

        }

    }
}

using UnityEngine;
namespace OUA.Utilities
{

    public class MusicPlayer : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioClip[] musicTracks;
        private int currentTrackIndex = 0;

        void Start()
        {
            if (audioSource == null)
            {
                Debug.LogError("AudioSource component is not assigned!");
                return;
            }

            if (musicTracks.Length == 0)
            {
                Debug.LogError("No music tracks assigned!");
                return;
            }

            PlayNextTrack();
        }

        void Update()
        {
            if (!audioSource.isPlaying)
            {
                PlayNextTrack();
            }
        }

        void PlayNextTrack()
        {
            if (musicTracks.Length == 0) return;

            // Play the current track
            audioSource.clip = musicTracks[currentTrackIndex];
            audioSource.Play();

            // Move to the next track index
            currentTrackIndex = (currentTrackIndex + 1) % musicTracks.Length;
        }
    }
}
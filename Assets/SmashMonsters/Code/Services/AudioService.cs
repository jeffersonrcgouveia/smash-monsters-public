using UnityEngine;

namespace SmashMonsters.Services
{
	public class AudioService : MonoBehaviour
	{
		/*----------------------------------------------------------------------------------------*
	     * Components
	     *----------------------------------------------------------------------------------------*/

		private AudioSource _backgroundAudioSource;

		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		private void Awake()
		{
			_backgroundAudioSource = gameObject.AddComponent<AudioSource>();
		}

		/*----------------------------------------------------------------------------------------*
	     * Methods
	     *----------------------------------------------------------------------------------------*/

		public void SetBackgroundMusic(AudioClip clip)
		{
			_backgroundAudioSource.clip = clip;
			_backgroundAudioSource.Play();
		}

		public void SetBackgroundMusicVolume(float volume)
		{
			_backgroundAudioSource.volume = volume;
		}

	}
}

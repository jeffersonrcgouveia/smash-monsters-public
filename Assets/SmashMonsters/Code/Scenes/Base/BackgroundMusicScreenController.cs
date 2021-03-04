using SmashMonsters.Services;
using UnityEngine;
using Zenject;

namespace SmashMonsters.Scenes.Base
{
	public class BackgroundMusicScreenController : BaseScreenController
	{
		/*----------------------------------------------------------------------------------------*
		 * Inject
		 *----------------------------------------------------------------------------------------*/

		[Inject]
		private AudioService _audioService;

		/*----------------------------------------------------------------------------------------*
		 * SerializeField
		 *----------------------------------------------------------------------------------------*/

		[SerializeField]
		private AudioClip backgroundMusic;

		/*----------------------------------------------------------------------------------------*
		 * Events
		 *----------------------------------------------------------------------------------------*/

		private void OnEnable()
		{
			_audioService.SetBackgroundMusic(backgroundMusic);
		}
	}
}
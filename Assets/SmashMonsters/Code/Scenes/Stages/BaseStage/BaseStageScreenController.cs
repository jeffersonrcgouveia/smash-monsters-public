using System.Linq;
using SmashMonsters.Scenes.Base;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace SmashMonsters.Scenes.Stages.BaseStage
{
	public class BaseStageScreenController : BackgroundMusicScreenController
	{
		/*----------------------------------------------------------------------------------------*
	     * Inject
	     *----------------------------------------------------------------------------------------*/

		[Inject]
		private GameState.GameState gameState;

		/*----------------------------------------------------------------------------------------*
		 * SerializeField
		 *----------------------------------------------------------------------------------------*/

		[SerializeField]
		private SceneAsset celebrationScreen;

		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		private void Start()
		{
			gameState.Winners.CollectionChanged += (sender, args) =>
			{
				int count = gameState.Winners.Count();
				if (count == 1)
				{
					LoadScene(celebrationScreen);
				}
			};
		}

	}
}

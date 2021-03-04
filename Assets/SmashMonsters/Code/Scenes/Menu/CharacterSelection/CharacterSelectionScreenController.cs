using SmashMonsters.Scenes.Base;
using UnityEngine;
using Zenject;

namespace SmashMonsters.Scenes.Menu.CharacterSelection
{
	public class CharacterSelectionScreenController : BaseScreenController
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
		private Canvas selectStageButtonCanvas;

	}
}

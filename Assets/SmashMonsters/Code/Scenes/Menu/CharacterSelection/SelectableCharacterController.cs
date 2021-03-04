using System;
using System.Collections.Generic;
using System.Linq;
using SmashMonsters.Code.Characters.Base;
using TopDownMedieval.Plugins.Commons.Utils;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using static SmashMonsters.GameState.GameState.Player;

namespace SmashMonsters.Scenes.Menu.CharacterSelection
{
	public class SelectableCharacterController : MonoBehaviour
	{
		/*----------------------------------------------------------------------------------------*
		 * Inject
		 *----------------------------------------------------------------------------------------*/

		[Inject]
		private GameState.GameState gameState;

		/*----------------------------------------------------------------------------------------*
		 * SerializeField
		 *----------------------------------------------------------------------------------------*/

		[field:SerializeField]
		public Character Character { get; set; }

		[field:SerializeField]
		public CanvasGroup SelectStageButtonCanvas { get; set; }

		[SerializeField]
		private Transform playersSelectedPanel;

		[field:SerializeField]
		public Image Thumbnail { get; set; }

		[field:SerializeField]
		public Text Label { get; set; }

		[SerializeField]
		private IndicatorController playerOneIndicator;

		[SerializeField]
		private IndicatorController playerTwoIndicator;

		[SerializeField]
		private IndicatorController playerTreeIndicator;

		[SerializeField]
		private IndicatorController playerFourIndicator;

		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		private void Start()
		{
			gameState.CharactersByPlayer.CollectionChanged += (sender, args) =>
			{
				foreach (KeyValuePair<GameState.GameState.Player, Character> newItem in args.NewItems)
				{
					gameState.Winners[newItem.Key] = newItem.Value;
				}

				if (gameState.CharactersByPlayer.Count() == 2)
				{
					CanvasUtils.Show(SelectStageButtonCanvas);
					Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
				}
			};
		}

		public void Cancel()
		{
			CanvasUtils.Hide(SelectStageButtonCanvas);
			gameState.CharactersByPlayer.Clear();
			for (int i = playersSelectedPanel.transform.childCount - 1; i >= 0; i--)
			{
				Destroy(playersSelectedPanel.transform.GetChild(i).gameObject);
			}
		}

		public void SelectCharacter(GameState.GameState.Player player, Character character)
		{
			gameState.CharactersByPlayer.Add(player, character);
		}

		public void SelectNextPlayerCharacter(Character character)
		{
			switch (gameState.CharactersByPlayer.AddNextEnumKey(character))
			{
				case PlayerOne:
					Instantiate(playerOneIndicator, playersSelectedPanel);
					break;
				case PlayerTwo:
					Instantiate(playerTwoIndicator, playersSelectedPanel);
					break;
				case PlayerTree:
					Instantiate(playerTreeIndicator, playersSelectedPanel);
					break;
				case PlayerFour:
					Instantiate(playerFourIndicator, playersSelectedPanel);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform) playersSelectedPanel.transform);
		}

		public void Select()
		{
			SelectNextPlayerCharacter(Character);
		}

	}
}

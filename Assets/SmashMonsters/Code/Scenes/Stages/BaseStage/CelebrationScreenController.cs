using System.Collections.Generic;
using System.Text;
using SmashMonsters.Code.Characters.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SmashMonsters.Scenes.Stages.BaseStage
{
	public class CelebrationScreenController : MonoBehaviour
	{
		/*----------------------------------------------------------------------------------------*
	     * Inject
	     *----------------------------------------------------------------------------------------*/

		[Inject]
		private GameState.GameState gameState;

		[Inject]
		private CharactersPrefabsController charactersPrefabs;

		/*----------------------------------------------------------------------------------------*
		 * SerializeField
		 *----------------------------------------------------------------------------------------*/

		[SerializeField]
		private TextMeshProUGUI winnerText;

		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		private void Start()
		{
			HandleWinner();
		}

		/*----------------------------------------------------------------------------------------*
	     * Methods
	     *----------------------------------------------------------------------------------------*/

		private void HandleWinner()
		{
			StringBuilder sb = new StringBuilder();
			foreach (KeyValuePair<GameState.GameState.Player, Character> pair in gameState.Winners)
			{
				GameObject characterPrefab = charactersPrefabs.CharactersPrefabByCharacter[pair.Value];
				CreateWinnerCharacterImage(characterPrefab).transform.SetParent(transform);
				AppendWinnerText(sb, pair.Key);
			}
			winnerText.text = sb.Append(" Wins!").ToString();
		}

		private static GameObject CreateWinnerCharacterImage(GameObject characterGO)
		{
			GameObject characterImage = new GameObject();
			characterImage.transform.localScale = new Vector3(5, 5, 5);
			Image image = characterImage.AddComponent<Image>();
			image.sprite = characterGO.GetComponent<SpriteRenderer>().sprite;
			image.preserveAspect = true;
			return characterImage;
		}

		private void AppendWinnerText(StringBuilder sb, GameState.GameState.Player player)
		{
			if (sb.Length > 0)
			{
				sb.Append(", ");
			}
			sb.Append(player.ToString());
		}

	}
}

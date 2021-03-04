using System.Collections.Generic;
using SmashMonsters.Code.Characters.Base;
using UnityEngine;
using Xamarin.Forms.Dynamic;

namespace SmashMonsters.GameState
{
	public class GameState : MonoBehaviour
	{
		/*----------------------------------------------------------------------------------------*
	     * Attributes
	     *----------------------------------------------------------------------------------------*/

		public ObservableDictionary<Player, Character> CharactersByPlayer { get; } =
			new ObservableDictionary<Player, Character>();

		public Dictionary<Player, GameObject> CharactersGOByPlayer { get; } = new Dictionary<Player, GameObject>();

		public int Lives { get; set; } = 5;
		public int Timer { get; set; } = 3;

		public ObservableDictionary<Player, Character> Winners { get; } = new ObservableDictionary<Player, Character>();

		/*----------------------------------------------------------------------------------------*
	     * Inner Classes and Delegates
	     *----------------------------------------------------------------------------------------*/

		public enum Player
		{
			PlayerOne,
			PlayerTwo,
			PlayerTree,
			PlayerFour
		}

	}
}

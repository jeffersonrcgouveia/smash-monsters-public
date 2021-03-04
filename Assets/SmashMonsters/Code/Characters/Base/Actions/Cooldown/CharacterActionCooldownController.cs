using System.Collections.Generic;
using SmashMonsters.Player.Input;
using UnityEngine;

namespace SmashMonsters.Code.Characters.Base.Actions.Cooldown
{
	public class CharacterActionCooldownController : MonoBehaviour
	{
		/*----------------------------------------------------------------------------------------*
	     * Constants
	     *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Injects
	     *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Components
	     *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Exposed Events
	     *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Exposed Variables
	     *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Variables
	     *----------------------------------------------------------------------------------------*/
    
		private readonly Dictionary<CharacterActionInput.InputAction, InputActionCooldown> _cooldownsByAction =
			new Dictionary<CharacterActionInput.InputAction, InputActionCooldown>();
	
		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		private void Update()
		{
			foreach (KeyValuePair<CharacterActionInput.InputAction, InputActionCooldown> pair in _cooldownsByAction)
			{
				InputActionCooldown actionCooldown = pair.Value;
				actionCooldown.CanAct = actionCooldown.TimeStamp <= Time.time;
			}
		}

		/*----------------------------------------------------------------------------------------*
	     * Animation Events
	     *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Methods
	     *----------------------------------------------------------------------------------------*/

		public void StartActionCooldown(CharacterActionInput.InputAction action, float cooldown)
		{
			if (!ContainsAction(action))
			{
				_cooldownsByAction.Add(action, new InputActionCooldown());
			}
			InputActionCooldown actionCooldown = _cooldownsByAction[action];
			actionCooldown.Cooldown = cooldown;
			actionCooldown.TimeStamp = actionCooldown.Cooldown + Time.time;
		}

		public bool ValidateCooldown(CharacterActionInput.InputAction action)
		{
			return !ContainsAction(action) || _cooldownsByAction[action].CanAct;
		}

		public bool ContainsAction(CharacterActionInput.InputAction action)
		{
			return _cooldownsByAction.ContainsKey(action);
		}
	
		/*----------------------------------------------------------------------------------------*
	     * Utility Methods
	     *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Coroutines
	     *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Inner Classes and Delegates
	     *----------------------------------------------------------------------------------------*/
    
		private class InputActionCooldown
		{
			public float TimeStamp { get; set; }
			public float Cooldown { get; set; }
			public bool CanAct { get; set; }
		}
	
	}
}

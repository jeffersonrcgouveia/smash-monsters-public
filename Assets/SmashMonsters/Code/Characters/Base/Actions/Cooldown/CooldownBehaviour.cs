using SmashMonsters.Player.Input;
using UnityEngine;

namespace SmashMonsters.Code.Characters.Base.Actions.Cooldown
{
	public class CooldownBehaviour : StateMachineBehaviour
	{
		/*----------------------------------------------------------------------------------------*
		 * Components
		 *----------------------------------------------------------------------------------------*/

		private CharacterInputController _inputController;

		private CharacterActionCooldownController _cooldownController;

		/*----------------------------------------------------------------------------------------*
		 * Exposed Variables
		 *----------------------------------------------------------------------------------------*/

		[field:SerializeField]
		public float Cooldown { get; set; }

		/*----------------------------------------------------------------------------------------*
		 * Events
		 *----------------------------------------------------------------------------------------*/

		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			PopulateComponents(animator);

			_cooldownController.StartActionCooldown(_inputController.Action.Action, Cooldown);
		}

		/*----------------------------------------------------------------------------------------*
		 * Methods
		 *----------------------------------------------------------------------------------------*/

		private void PopulateComponents(Animator animator)
		{
			if (_inputController != null) return;
			_inputController = animator.GetComponent<CharacterInputController>();
			_cooldownController = animator.GetComponent<CharacterActionCooldownController>();
		}

	}
}

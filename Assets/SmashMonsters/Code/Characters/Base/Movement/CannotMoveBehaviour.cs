using UnityEngine;

namespace SmashMonsters.Code.Characters.Base.Movement
{
	public class CannotMoveBehaviour : StateMachineBehaviour
	{
		/*----------------------------------------------------------------------------------------*
		 * Components
		 *----------------------------------------------------------------------------------------*/

		private CharacterMovementController _characterMovementController;

		/*----------------------------------------------------------------------------------------*
		 * Events
		 *----------------------------------------------------------------------------------------*/

		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (_characterMovementController == null)
			{
				_characterMovementController = animator.transform.GetComponent<CharacterMovementController>();
			}

			_characterMovementController.CanMove.Value = false;
		}

		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			_characterMovementController.CanMove.Value = true;
		}
	}
}

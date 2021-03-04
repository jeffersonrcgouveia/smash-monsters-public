using UnityEngine;

namespace SmashMonsters.Code.Characters.Base.Movement
{
	public class StopMovingAndTurningBehaviour : StateMachineBehaviour
	{
		/*----------------------------------------------------------------------------------------*
		 * Components
		 *----------------------------------------------------------------------------------------*/

		private CharacterMovementController _movementController;

		private CharacterTurnController _turnController;

		private CharacterGroundedController _groundedController;

		/*----------------------------------------------------------------------------------------*
		 * Exposed Variables
		 *----------------------------------------------------------------------------------------*/

		/*----------------------------------------------------------------------------------------*
		 * Events
		 *----------------------------------------------------------------------------------------*/

		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			PopulateComponents(animator);

			SetCanMoveAndTurn(false);
		}

		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			SetCanMoveAndTurn(true);
		}

		/*----------------------------------------------------------------------------------------*
		 * Methods
		 *----------------------------------------------------------------------------------------*/

		private void PopulateComponents(Animator animator)
		{
			if (_movementController != null) return;
			_movementController = animator.GetComponent<CharacterMovementController>();
			_turnController = animator.GetComponent<CharacterTurnController>();
			_groundedController = animator.GetComponent<CharacterGroundedController>();
		}

		private void SetCanMoveAndTurn(bool canMoveAndTurn)
		{
			if (_groundedController.IsGrounded)
			{
				_movementController.CanMove.Value = canMoveAndTurn;
			}

			_turnController.CanTurn.Value = canMoveAndTurn;
		}

	}
}

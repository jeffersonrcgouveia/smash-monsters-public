using UnityEngine;

namespace SmashMonsters.Code.Characters.Base.Actions.Attack
{
	public class AttackBehaviour : StateMachineBehaviour
	{
		/*----------------------------------------------------------------------------------------*
		 * Components
		 *----------------------------------------------------------------------------------------*/

		private CharacterHitBoxController[] _hitBoxControllers;

		/*----------------------------------------------------------------------------------------*
		 * Exposed Variables
		 *----------------------------------------------------------------------------------------*/

		[field:SerializeField]
		public virtual AttackInfo AttackInfo { get; set; }

		/*----------------------------------------------------------------------------------------*
		 * Events
		 *----------------------------------------------------------------------------------------*/

		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			PopulateComponents(animator);
		
			foreach (CharacterHitBoxController hitBoxController in _hitBoxControllers)
			{
				hitBoxController.AttackInfo = AttackInfo;
			}
		}

		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			foreach (CharacterHitBoxController hitBoxController in _hitBoxControllers)
			{
				hitBoxController.CanAttack = true;
			}
		}

		/*----------------------------------------------------------------------------------------*
		 * Methods
		 *----------------------------------------------------------------------------------------*/

		private void PopulateComponents(Animator animator)
		{
			if (_hitBoxControllers != null) return;
			_hitBoxControllers = animator.GetComponentsInChildren<CharacterHitBoxController>(true);
		}

		/*----------------------------------------------------------------------------------------*
	     * Inner Classes and Delegates
	     *----------------------------------------------------------------------------------------*/

	}
}

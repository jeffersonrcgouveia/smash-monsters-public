using UnityEngine;

namespace SmashMonsters.Code.Characters.Base.Hurt
{
	public class HurtFallingBehaviour : StateMachineBehaviour
	{
		/*----------------------------------------------------------------------------------------*
		 * Components
		 *----------------------------------------------------------------------------------------*/

		private Animator _animator;

		/*----------------------------------------------------------------------------------------*
		 * Inject
		 *----------------------------------------------------------------------------------------*/

		/*----------------------------------------------------------------------------------------*
		 * Events
		 *----------------------------------------------------------------------------------------*/

		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (_animator == null)
			{
				_animator = animator.transform.GetComponent<Animator>();
			}

			_animator.SetBool("IsTakingKnockback", false);
		}
	}
}

using SmashMonsters.Code.Characters.Base.Hurt;
using SmashMonsters.Code.Characters.Base.Movement;
using UnityEngine;

namespace SmashMonsters.Code.Characters.Base.Actions.Attack
{
	[ExecuteInEditMode]
	public class CharacterAttackController : MonoBehaviour
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

		private CharacterTurnController _turnController;
    
		/*----------------------------------------------------------------------------------------*
	     * Exposed Events
	     *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Exposed Variables
	     *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Variables
	     *----------------------------------------------------------------------------------------*/
    
		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		private void Awake()
		{
			_turnController = GetComponent<CharacterTurnController>();
		}

		/*----------------------------------------------------------------------------------------*
	     * Animation Events
	     *----------------------------------------------------------------------------------------*/

		/*----------------------------------------------------------------------------------------*
	     * Methods
	     *----------------------------------------------------------------------------------------*/

		public void Attack(AttackInfo attackInfo, GameObject enemy)
		{
			CharacterHurtController hurtController = enemy.GetComponentInParent<CharacterHurtController>();
			Vector2 attackDirection = CalculateAttackDirection(attackInfo);
			hurtController.TakeDamage(attackInfo, attackDirection);
		}
	
		private Vector2 CalculateAttackDirection(AttackInfo attackInfo)
		{
			return ChangeXAxisByCharacterFacing(attackInfo.Direction);
		}
	
		private Vector2 ChangeXAxisByCharacterFacing(Vector2 vector)
		{
			bool isFacingLeft = _turnController.IsFacingLeft;
			float x = isFacingLeft ? -vector.x : vector.x;
			return new Vector2(x, vector.y);
		}
	
		/*----------------------------------------------------------------------------------------*
	     * Inner Classes and Delegates
	     *----------------------------------------------------------------------------------------*/

	}
}

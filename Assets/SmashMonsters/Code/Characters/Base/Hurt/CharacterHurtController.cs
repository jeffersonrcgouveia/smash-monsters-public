using System.Collections;
using AvoEx.ObservableType;
using SmashMonsters.Code.Characters.Base.Actions.Attack;
using UnityEngine;

namespace SmashMonsters.Code.Characters.Base.Hurt
{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(Rigidbody2D))]
	public class CharacterHurtController : MonoBehaviour
	{
		/*----------------------------------------------------------------------------------------*
		 * Constants
		 *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
		 * Components
		 *----------------------------------------------------------------------------------------*/

		private Animator _animator;

		private Rigidbody2D _rigidbody;

		/*----------------------------------------------------------------------------------------*
	     * Exposed Variables
	     *----------------------------------------------------------------------------------------*/

		public ObFloat Damage { get; } = new ObFloat();

		public ObInt Lives { get; } = new ObInt();

		[SerializeField]
		private float weight;

		/*----------------------------------------------------------------------------------------*
		 * Events
		 *----------------------------------------------------------------------------------------*/

		private void Awake()
		{
			_animator = GetComponent<Animator>();
			_rigidbody = GetComponent<Rigidbody2D>();
		}

		/*----------------------------------------------------------------------------------------*
	     * Methods
	     *----------------------------------------------------------------------------------------*/

		public void TakeDamage(AttackInfo info, Vector2 direction = default)
		{
			Damage.Value += info.Damage;
			Debug.Log($"Damage.Value: {Damage.Value}");
			_animator.SetTrigger("TakeDamage");
			if (info.KnockbackPower > 0)
			{
				TakeKnockback(info, direction);
			}
		}

		private void TakeKnockback(AttackInfo info, Vector2 attackDir)
		{
			if (info.KnockbackPower > 10)
			{
				_animator.SetBool("IsTakingKnockback", true);
			}
		
			float knockback = ((Damage / 10 + Damage * info.Damage / 20) * (200 / (weight + 100) * 1.4f) + 18) *
				(info.KnockbackScaling / 100) + info.KnockbackPower;

			Debug.Log($"knockback: {knockback}");
		
			_rigidbody.AddForce(attackDir.normalized * (knockback * 0.1f), ForceMode2D.Impulse);
		}

		public bool RemoveLife()
		{
			if (Lives.Value <= 0) return false;
			Lives.Value--;
			Damage.Value = 0;
			return true;
		}

		public void GetUp()
		{
			StartCoroutine(GetUpCoroutine());
		}

		/*----------------------------------------------------------------------------------------*
	     * Coroutines
	     *----------------------------------------------------------------------------------------*/

		private IEnumerator GetUpCoroutine()
		{
			yield return new WaitForSeconds(3);
			_animator.SetTrigger("IsGettingUp");
		}

	}
}

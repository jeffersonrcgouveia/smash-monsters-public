using System.Collections;
using AvoEx.ObservableType;
using UnityEngine;

namespace SmashMonsters.Code.Characters.Base.Projectiles
{
	public class ProjectileController : MonoBehaviour
	{
		/*----------------------------------------------------------------------------------------*
	     * SerializeField
	     *----------------------------------------------------------------------------------------*/

		[SerializeField]
		protected float lifeTimeInSeconds = 2;

		[field:SerializeField]
		public ObBool WaitToDestroy { get; } = new ObBool();

		[SerializeField]
		private float moveSpeed = 100f;
    
		/*----------------------------------------------------------------------------------------*
	     * Attributes
	     *----------------------------------------------------------------------------------------*/

		public Vector3 Direction { get; set; }

		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		protected virtual void Start()
		{
			if (!WaitToDestroy.Value)
			{
				StartCoroutine(DestroyGameObjectCoroutine());
			}
			else
			{
				WaitToDestroy.AddObserver(wait =>
				{
					StartCoroutine(DestroyGameObjectCoroutine());
				});
			}

			if (moveSpeed > 0)
			{
				Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
				rigidbody.AddForce(Direction * moveSpeed, ForceMode2D.Impulse);
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			Destroy(gameObject);
		}

		/*----------------------------------------------------------------------------------------*
	     * Coroutines
	     *----------------------------------------------------------------------------------------*/

		private IEnumerator DestroyGameObjectCoroutine()
		{
			yield return new WaitForSeconds(lifeTimeInSeconds);
			Destroy(gameObject);
		}
	}
}

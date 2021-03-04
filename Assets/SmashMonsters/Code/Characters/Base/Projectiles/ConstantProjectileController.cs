using UnityEngine;

namespace SmashMonsters.Code.Characters.Base.Projectiles
{
	public class ConstantProjectileController : ProjectileController
	{
		/*----------------------------------------------------------------------------------------*
	     * SerializeField
	     *----------------------------------------------------------------------------------------*/

		[SerializeField]
		private float speed = 6f;

		[field:SerializeField]
		public bool CanMove { get; set; } = true;
    
		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/
    
		private void Update()
		{
			if (CanMove)
			{
				HandleMovement();
			}
		}
    
		/*----------------------------------------------------------------------------------------*
	     * Methods
	     *----------------------------------------------------------------------------------------*/

		private void HandleMovement()
		{
//        RotateAround();
			Vector3 direction = (transform.rotation * Vector3.right * -1).normalized;
			float directionAxis = direction.y != 0 ? direction.y : direction.x;
			transform.position += new Vector3(speed * directionAxis * Time.deltaTime, 0, 0);
		}

		private void RotateAround()
		{
			transform.RotateAround(transform.position, Vector3.forward, 1000 * Time.deltaTime);
		}
	}
}

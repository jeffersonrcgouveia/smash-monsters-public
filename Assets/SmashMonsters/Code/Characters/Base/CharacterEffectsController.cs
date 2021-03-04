using UnityEngine;

namespace SmashMonsters.Code.Characters.Base
{
	public class CharacterEffectsController : MonoBehaviour
	{
		/*----------------------------------------------------------------------------------------*
	     * Components
	     *----------------------------------------------------------------------------------------*/

		private SpriteRenderer _renderer;
		private Collider _collider;
		private Rigidbody _rigidbody;
    
		/*----------------------------------------------------------------------------------------*
	     * Inject
	     *----------------------------------------------------------------------------------------*/

		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		private void Start()
		{
			_renderer = GetComponent<SpriteRenderer>();
			_collider = GetComponent<Collider>();
			_rigidbody = GetComponent<Rigidbody>();
		}

		/*----------------------------------------------------------------------------------------*
	     * Methods
	     *----------------------------------------------------------------------------------------*/

		public void Freeze()
		{
			_renderer.color = Color.blue;
			_collider.enabled = false;
			_rigidbody.useGravity = false;
			_rigidbody.isKinematic = true;
		}

	}
}

using UnityEngine;

namespace SmashMonsters.Scenes.Base
{
	public class LoadingScreenController : MonoBehaviour
	{	
		/*----------------------------------------------------------------------------------------*
	     * Constants
	     *----------------------------------------------------------------------------------------*/

		private const float TurnSpeed = 200f;
    
		/*----------------------------------------------------------------------------------------*
	     * SerializeField
	     *----------------------------------------------------------------------------------------*/

		[SerializeField]
		private Transform loadingImage;
	
		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
		}

		private void Update()
		{
			loadingImage.Rotate(0, 0, -TurnSpeed * Time.fixedDeltaTime);
		}

	}
}

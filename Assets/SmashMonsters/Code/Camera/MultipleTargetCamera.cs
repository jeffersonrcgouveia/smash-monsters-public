using System.Collections.Generic;
using UnityEngine;

namespace SmashMonsters.Code.Camera
{
	public class MultipleTargetCamera : MonoBehaviour
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
	
		/*----------------------------------------------------------------------------------------*
	     * Exposed Events
	     *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Exposed Variables
	     *----------------------------------------------------------------------------------------*/

		[SerializeField]
		private FocusLevel focusLevel;

		[SerializeField]
		private List<Transform> players;

		[SerializeField]
		private float depthUpdateSpeed = 5f;
	
		[SerializeField]
		private float angleUpdateSpeed = 7f;
	
		[SerializeField]
		private float positionUpdateSpeed = 5f;

		[SerializeField]
		private float depthMax = -10f;
	
		[SerializeField]
		private float depthMin = -22f;

		[SerializeField]
		private float angleMax = 11f;
	
		[SerializeField]
		private float angleMin = 3f;

		/*----------------------------------------------------------------------------------------*
	     * Variables
	     *----------------------------------------------------------------------------------------*/

		private float _cameraEulerX;
	
		private Vector3 _cameraPosition;
	
		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		private void Start()
		{
			AddPlayer(focusLevel.transform);
		}

		private void LateUpdate()
		{
			CalculateCameraLocations();
			MoveCamera();
		}

		/*----------------------------------------------------------------------------------------*
	     * Animation Events
	     *----------------------------------------------------------------------------------------*/

		/*----------------------------------------------------------------------------------------*
	     * Methods
	     *----------------------------------------------------------------------------------------*/

		public void AddPlayer(Transform player)
		{
			players.Add(player);
		}

		private void CalculateCameraLocations()
		{
			Vector3 averageCenter = Vector3.zero;
			Vector3 totalPositions = Vector3.zero;
			Bounds playerBounds = new Bounds();

			foreach (Transform player in players)
			{
				Vector3 playerPos = CalculatePlayerPosition(player);

				totalPositions += playerPos;
				playerBounds.Encapsulate(playerPos);
			}

			averageCenter = totalPositions / players.Count;

			float lerpPercent = CalculateLerpPercent(playerBounds);

			float depth = Mathf.Lerp(depthMax, depthMin, lerpPercent);
			float angle = Mathf.Lerp(angleMax, angleMin, lerpPercent);

			_cameraEulerX = angle;
			_cameraPosition = new Vector3(averageCenter.x, averageCenter.y, depth);
		}

		private void MoveCamera()
		{
			Vector3 pos = transform.position;
			if (pos != _cameraPosition)
			{
				Vector3 targetPos = Vector3.zero;
				targetPos.x = Mathf.MoveTowards(pos.x, _cameraPosition.x, positionUpdateSpeed * Time.deltaTime);
				targetPos.y = Mathf.MoveTowards(pos.y, _cameraPosition.y, positionUpdateSpeed * Time.deltaTime);
				targetPos.z = Mathf.MoveTowards(pos.z, _cameraPosition.z, depthUpdateSpeed * Time.deltaTime);
				transform.position = targetPos;
			}

			Vector3 eulerAngles = transform.localEulerAngles;
			if (eulerAngles.x == _cameraEulerX) return;
			Vector3 targetEulerAngles = new Vector3(_cameraEulerX, eulerAngles.y, eulerAngles.z);
			transform.localEulerAngles = Vector3.MoveTowards(eulerAngles, targetEulerAngles, angleUpdateSpeed * Time.deltaTime);
		}

		private Vector3 CalculatePlayerPosition(Transform player)
		{
			Vector3 playerPos = player.position;

			if (!focusLevel.FocusBounds.Contains(playerPos))
			{
				playerPos = ClampPositionToBounds(playerPos, focusLevel.FocusBounds);
			}

			return playerPos;
		}

		private Vector3 ClampPositionToBounds(Vector3 position, Bounds bounds)
		{
			float x = Mathf.Clamp(position.x, bounds.min.x, bounds.max.x);
			float y = Mathf.Clamp(position.y, bounds.min.y, bounds.max.y);
			float z = Mathf.Clamp(position.z, bounds.min.z, bounds.max.z);

			return new Vector3(x, y, z);
		}

		private float CalculateLerpPercent(Bounds playerBounds)
		{
			float extents = playerBounds.extents.x + playerBounds.extents.y;
			return Mathf.InverseLerp(0, (focusLevel.HalfBounds.x + focusLevel.HalfBounds.y) / 2, extents);
		}
	
		/*----------------------------------------------------------------------------------------*
	     * Utility Methods
	     *----------------------------------------------------------------------------------------*/

		/*----------------------------------------------------------------------------------------*
	     * Coroutines
	     *----------------------------------------------------------------------------------------*/

		/*----------------------------------------------------------------------------------------*
	     * Inner Classes and Delegates
	     *----------------------------------------------------------------------------------------*/

	}
}

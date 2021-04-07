using UnityEngine;

namespace Modding.API {
	public class ThirdPersonAPI {
		public static void ActivateThirdPerson() {
			if (Managers.CameraManager == null || Managers.CameraManager.tpCam == null || Managers.CameraManager.playerFollowCam == null)
				return;

			Managers.CameraManager.tpCam.enabled = true;
			Managers.CameraManager.tpCam.Priority = 200;
			Managers.CameraManager.playerFollowCam.Priority = 90;
			Cursor.lockState = CursorLockMode.Locked;
		}

		public static void DeactivateThirdPerson() {
			if (Managers.CameraManager == null || Managers.CameraManager.tpCam == null || Managers.CameraManager.playerFollowCam == null)
				return;

			Managers.CameraManager.tpCam.enabled = false;
			Managers.CameraManager.tpCam.Priority = 10;
			Managers.CameraManager.playerFollowCam.Priority = 100;
			Cursor.lockState = CursorLockMode.None;
		}
	}
}

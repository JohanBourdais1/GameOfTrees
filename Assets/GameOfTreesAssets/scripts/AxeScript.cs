using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class AxeScript : MonoBehaviour
{
	InputDevice RightControllerDevice;
	Vector3 RightControllerVelocity;

	public float minCollisionVelocity = 5f;

	// Start is called before the first frame update
	void Start()
	{
		RightControllerDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
	}

	void OnCollisionEnter(Collision collision)
	{
		///this is dumb when attached to a vr controller the axe will always have a zero velocity so we take the velocity of the controller and use it to determine the force of the collision
		RightControllerDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out RightControllerVelocity);

		float collisionVelocity = RightControllerVelocity.magnitude;

		if (collisionVelocity >= minCollisionVelocity)
		{
			// If the component exists, call the function on it
			if (collision.gameObject.TryGetComponent(out TreeScript component))
			{
				// Send a haptic impulse
				if (RightControllerDevice.TryGetHapticCapabilities(out HapticCapabilities capabilities) && capabilities.supportsImpulse)
					RightControllerDevice.SendHapticImpulse(0u, Mathf.Min((collisionVelocity - minCollisionVelocity) * 2f, 1f), 0.3f);

				if (TryGetComponent(out AudioSource source))
					source.Play();

				component.takeDamage();
			}
		}
	}
}

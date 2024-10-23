using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class TreeFalling : MonoBehaviour
{
	public float MaxHapticDistance = 20f;
	public AudioClip FallingSound;
	public AudioClip ImpactSound;

	bool Impact = false;
	AudioSource AudioSource;

    public void PlayFallingSound()
	{
		if (TryGetComponent(out AudioSource))
		{
			AudioSource.loop = true;
			AudioSource.clip = FallingSound;
			AudioSource.Play();
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if ((collision.gameObject.TryGetComponent(out TerrainCollider _) || collision.gameObject.CompareTag("terrain")) && !Impact)
		{
			Impact = true;
			if (InputDevices.GetDeviceAtXRNode(XRNode.LeftHand) != null && InputDevices.GetDeviceAtXRNode(XRNode.RightHand) != null)
			{
				var axeObj = FindObjectOfType<AxeScript>();

                if (axeObj != null)
                {
					float amplitude = (MaxHapticDistance - (gameObject.transform.position - axeObj.gameObject.transform.position).magnitude) / MaxHapticDistance;
					if (InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetHapticCapabilities(out HapticCapabilities rightCapabilities) && rightCapabilities.supportsImpulse)
					{
						if (amplitude > 0f && amplitude < 1f)
							InputDevices.GetDeviceAtXRNode(XRNode.RightHand).SendHapticImpulse(0u, amplitude, 1f);
					}
					if (InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetHapticCapabilities(out HapticCapabilities leftCapabilities) && leftCapabilities.supportsImpulse)
					{
						if (amplitude > 0f && amplitude < 1f)
							InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).SendHapticImpulse(0u, amplitude, 1f);
					}
				}
			}

			if (AudioSource)
			{
				AudioSource.Stop();
				AudioSource.loop = false;
				AudioSource.clip = ImpactSound;
				AudioSource.Play();
				StartCoroutine(SoundEnded());
			}

           var a =  GameObject.Find("_livingBirdsController");
		   if(a != null)
		   {
			a.TryGetComponent<lb_BirdController>(out lb_BirdController component);
		   
			if (component != null)
			{
				component.AllFlee();
			}
		   }
        }
	}

	IEnumerator SoundEnded()
	{
		yield return new WaitUntil(() => !AudioSource.isPlaying);

		Destroy(AudioSource);
		Destroy(this);
	}
}

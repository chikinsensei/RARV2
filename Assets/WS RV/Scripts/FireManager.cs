using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class FireManager : MonoBehaviour
{
	[SerializeField, Range(0f, 10f)] private float currentIntensity = 10.0f;
	private float startIntensity = 10.0f;

	[SerializeField] private ParticleSystem explosionParticles;
	[SerializeField] private ParticleSystem fireParticles;
	[SerializeField] private Rigidbody headRigidbody;
	[SerializeField] private XRGrabInteractable headGrab;

	public GameObject robot;
	public bool isOnFire = false;
	bool exploded = false;


    public GameObject socket;
    bool isSocketed = false;

    private void Start()
	{
		startIntensity = fireParticles.emission.rateOverTime.constant;
		explosionParticles.Stop();
		fireParticles.Stop();
		headGrab.enabled = false;
	}

	private void Update()
	{
		ChangeIntensity();
		// D�clencheur d'incendie
		if (Keyboard.current.fKey.wasPressedThisFrame && !exploded)
		{
			StartFire();
			isOnFire = true;
			exploded = true;
		}
	}

	private void StartFire()
	{
		socket.SetActive(isSocketed);
		// Faire sauter la t�te du robot
		headRigidbody.AddForce(Vector3.up * 5f + Vector3.forward * 1f, ForceMode.Impulse);

		// Activer les particules
		explosionParticles.Play();
		fireParticles.Play();
		robot.GetComponent<NavMeshAgent>().speed = 0;

		// Activer le socket apr�s l'explosion
		StartCoroutine(ActivateSocket());
		isSocketed = true;
	}

	IEnumerator ActivateSocket()
	{
		// Attendre 2 secondes pour �viter que le socket ne soit pendant l'explosion et que le collider soit trigger
        yield return new WaitForSeconds(2);
        socket.SetActive(isSocketed);
    }

	public bool TryExtinguish(float amount)
	{
		currentIntensity -= amount;
		Debug.Log(currentIntensity);
		ChangeIntensity();

		if(currentIntensity <= 0)
		{
			isOnFire = false;
			headGrab.enabled = true;
			return true;
		}
		return false; //fire out
	}


	private void ChangeIntensity()
	{
		var emission = fireParticles.emission;
		emission.rateOverTime = currentIntensity * startIntensity;
	}
}
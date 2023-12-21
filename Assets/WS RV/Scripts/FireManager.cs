using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class FireManager : MonoBehaviour
{
	[SerializeField, Range(0f, 10f)] private float currentIntensity = 10.0f;
	private float startIntensity = 10.0f;

	[SerializeField] private ParticleSystem explosionParticles;
	[SerializeField] private ParticleSystem fireParticles;
	[SerializeField] private Rigidbody headRigidbody;
	[SerializeField] private float amountExtinguishedPerSecond = 1f;
	public GameObject robot;
	bool isOnFire = false;

    public GameObject socket;
    bool isSocketed = false;

    private void Start()
	{
		startIntensity = fireParticles.emission.rateOverTime.constant;
		explosionParticles.Stop();
		fireParticles.Stop();
	}

	private void Update()
	{
		ChangeIntensity();
		// Déclencheur d'incendie
		if (Keyboard.current.fKey.wasPressedThisFrame && !isOnFire)
		{
			StartFire();
			isOnFire = true;
		}
	}

	private void StartFire()
	{
		// Faire sauter la tête du robot
		headRigidbody.AddForce(Vector3.up * 5f + Vector3.forward * 1f, ForceMode.Impulse);

		// Activer les particules
		explosionParticles.Play();
		fireParticles.Play();
		robot.GetComponent<NavMeshAgent>().speed = 0;

		// Activer le socket après l'explosion
		StartCoroutine(ActivateSocket());
		isSocketed = true;
	}

	IEnumerator ActivateSocket()
	{
		// Attendre 2 secondes pour éviter que le socket ne soit pendant l'explosion et que le collider soit trigger
        yield return new WaitForSeconds(2);
        socket.SetActive(isSocketed);
    }

	public bool TryExtinguish(float amount)
	{
		currentIntensity -= amount;
		ChangeIntensity();
		return currentIntensity <= 0; //fire out
	}

	private void ChangeIntensity()
	{
		var emission = fireParticles.emission;
		emission.rateOverTime = currentIntensity * startIntensity;
	}
}
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
	public GameObject robot;

	bool isOnFire = false;
	bool exploded = false;
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
		if (Keyboard.current.fKey.wasPressedThisFrame && !exploded)
		{
			StartFire();
			isOnFire = true;
			exploded = true;
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
	}

	public bool TryExtinguish(float amount)
	{
		currentIntensity -= amount;
		ChangeIntensity();

		if(currentIntensity <= 0)
		{
			isOnFire = false;
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
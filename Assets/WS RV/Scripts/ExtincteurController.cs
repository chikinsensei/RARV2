using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExtincteurController : MonoBehaviour
{
	[SerializeField]
	private InputActionReference trigger;

    [SerializeField]
    private ParticleSystem extinguishedParticle;

	[SerializeField]
	private FireManager fire;

	[SerializeField] private float amountExtinguishedPerSecond = 1f;

	// Start is called before the first frame update
	void Start()
    {
		extinguishedParticle.Stop();
	}

    // Update is called once per frame
    void Update()
    {
		if (trigger.action.IsPressed())
		{
			extinguishedParticle.Play();
			if (Physics.Raycast(extinguishedParticle.transform.position, extinguishedParticle.transform.forward, out RaycastHit hit, 100f)
			&& hit.collider.TryGetComponent(out FireManager fire))
			{
				Debug.Log(hit.collider.name);
				fire.TryExtinguish(amountExtinguishedPerSecond * Time.deltaTime);
			}
		}
		else
		{
			extinguishedParticle.Stop();
		}
	}

}

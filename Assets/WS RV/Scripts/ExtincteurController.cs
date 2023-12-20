using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ExtincteurController : MonoBehaviour
{
	[SerializeField]
	private InputActionReference trigger;

	[SerializeField]
    private ParticleSystem extinguishedParticle;

	[SerializeField] private float amountExtinguishedPerSecond = 5f;

	// Start is called before the first frame update
	void Start()
    {
		extinguishedParticle.Stop();
	}

    // Update is called once per frame
    void Update()
    {
		if (trigger.action.IsPressed() && transform.GetComponent<XRGrabInteractable>().isSelected)
		{
			extinguishedParticle.Play();
			if (Physics.Raycast(extinguishedParticle.transform.position, extinguishedParticle.transform.forward, out RaycastHit hit, 100f)
			&& hit.collider.TryGetComponent(out FireManager fire))
			{
				fire.TryExtinguish(amountExtinguishedPerSecond * Time.deltaTime);
			}
		}
		else
		{
			extinguishedParticle.Stop();
		}
	}

}

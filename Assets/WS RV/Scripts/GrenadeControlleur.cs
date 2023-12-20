using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeControlleur : MonoBehaviour
{
	[SerializeField]
	private GameObject grenade;

	[SerializeField]
	private ParticleSystem extinguishedParticle;

	[SerializeField] private float amountExtinguishedPerSecond = 10f;

	private bool grenadeUsed = false;
	// Start is called before the first frame update
	void Start()
    {
		extinguishedParticle.Stop();
	}

    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out FireManager fire) && !grenadeUsed)
		{
			extinguishedParticle.Play();
			fire.TryExtinguish(amountExtinguishedPerSecond);
			grenadeUsed = true;
			//GameObject.Destroy(grenade);
		}
	}
}

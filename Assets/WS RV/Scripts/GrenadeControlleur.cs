using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeControlleur : MonoBehaviour
{
	[SerializeField]
	private ParticleSystem extinguishedParticle;

	[SerializeField] private float amountExtinguishedPerSecond = 10f;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.TryGetComponent(out FireManager fire))
		{
			extinguishedParticle.Play();
			fire.TryExtinguish(amountExtinguishedPerSecond * Time.deltaTime);
			GameObject.Destroy(GameObject.Find("GrenadeIncendie"));
		}
	}
}

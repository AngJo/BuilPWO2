using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    
    private Rigidbody animalRb;

    public Transform hand;
    public GameObject animal;
    public Vector3 throwDirection;

    public GameObject bulletEmitter;
    public GameObject bullet;
    public GameObject bulletMilk;
    public float bulletForce;


    public float damage = 10f;
    public float range = 100f;
	// Use this for initialization
	void Start () {
        //Zit er iets in mijn hand
        if (hand.childCount > 0)
        {
            animal = hand.GetChild(0).gameObject;
            
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (hand.childCount > 0)
        {
            animalRb = animal.GetComponent<Rigidbody>();
            
            if (Input.GetMouseButtonDown(1))
            {
                animal.transform.parent = null;
                
                animalRb.isKinematic = false;
                animalRb.AddForce(throwDirection, ForceMode.Impulse);
            }

            else if (Input.GetMouseButtonDown(0))
            {
                GameObject tempBullet;
                tempBullet = Instantiate(bullet, bulletEmitter.transform.position, bulletEmitter.transform.rotation) as GameObject;
                RaycastHit hit;
             
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
                {

             //       tempBullet.transform.rotation = hit.transform.rotation;
                    /*Target target = hit.transform.GetComponent<Target>();
                    if (target != null)
                    {
                        target.TakeDamage(damage);
                    }*/
                }
                //Vector3 positionToLookat = transform.forward * 10;
              

                Rigidbody tempRb;
                tempRb = tempBullet.GetComponent<Rigidbody>();

                //Quaternion rot = Quaternion.LookRotation(positionToLookat, Vector3.up);

                //tempBullet.transform.rotation = rot;

                tempRb.AddForce(tempBullet.transform.forward * bulletForce);
                Destroy(tempBullet, 10.0f);
                
            }
        }

        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2))
                {
                    if (hit.collider.CompareTag("Animal"))
                    {
                        hit.transform.SetParent(hand, false);
                        hit.transform.localPosition = Vector3.zero;
                       
                        animal = hit.collider.gameObject;
                        Debug.Log(animal);
                    }
                }
            }

            
        }
	}
}

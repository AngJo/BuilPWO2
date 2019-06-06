using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    
    private Rigidbody animalRb;

    public Transform hand;
    public GameObject animal;
    //public Vector3 throwDirection;
    

    public GameObject bulletEmitter;
    public GameObject bullet;
    public GameObject bulletMilk;
    public GameObject bulletEgg;
    public GameObject bulletWheat;
    public float bulletForce;

    public GameObject bulletEmitterChicken;
    public GameObject bulletEmitterCow;
    public GameObject bulletEmitterBread;
    public GameObject bulletEmitterFire;


    public float damage = 10f;
    public float range = 100f;
    public bool alive;
	// Use this for initialization
	void Start () {
        alive = true;
        //Zit er iets in mijn hand
        if (hand.childCount > 0)
        {
            animal = hand.GetChild(0).gameObject;
            Debug.Log(animal.name);
            
        }
	}
	
	// Update is called once per frame
	void Update () {

        //If I have something in my hand
        if (hand.childCount > 0)
        {
            animalRb = animal.GetComponent<Rigidbody>();

            //Throw away the Animal
            if (Input.GetMouseButtonDown(1))
            {
                animal.transform.parent = null;
                
                animalRb.isKinematic = false;
                animalRb.AddForce(Camera.main.transform.forward * 10f, ForceMode.Impulse);
            }

            //Check Which Animal I have and shoot a bullet
            else if (Input.GetMouseButtonDown(0))
            {
                CheckAnimal(animal);
                GameObject tempBullet;
                ChangeEmitterValues(animal);
                //bulletEmitter.transform.SetParent(animal.transform, false);
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
                
                tempRb.AddForce(tempBullet.transform.forward * bulletForce);
                Destroy(tempBullet, 10.0f);
                
            }
        }

        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Grabbing New Animal");
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2))
                {

                    Debug.Log(hit.collider.name);
                    if (hit.collider.CompareTag("Animal"))
                    {
                        hit.transform.SetParent(hand, false);
                        hit.transform.localPosition = Vector3.zero;
                        animalRb = hit.transform.GetComponent<Rigidbody>();
                        animalRb.isKinematic = true;
                        hit.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                        if (hit.collider.name == "Bread") { hit.transform.localPosition = new Vector3(-0.098f, 0.462f, 0); }
                        else { hit.transform.localPosition = Vector3.zero; }
                       
                        animal = hit.collider.gameObject;
                        Debug.Log(animal);
                    }
                }
            }

            
        }

        
	}

    void CheckAnimal(GameObject animal)
    {
        switch (animal.name)
        {
            case "ChickenBrown": bullet = bulletEgg; break;
            case "CowBlW": bullet = bulletMilk; break;
            case "Bread": bullet = bulletWheat; break;
            default: bullet = bulletEgg; break;
        }
    }

    void ChangeEmitterValues(GameObject animal)
    {

        switch (animal.name)
        {
            case "ChickenBrown": bulletEmitter = bulletEmitterChicken; break;
            case "CowBlW": bulletEmitter = bulletEmitterCow; break;
            case "Bread": bulletEmitter = bulletEmitterBread; break;
            default:
                Debug.Log("Using Default");
                bulletEmitter = bulletEmitterChicken; break;
        }
    }
}

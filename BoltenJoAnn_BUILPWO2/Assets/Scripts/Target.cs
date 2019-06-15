using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public GameObject ingredientPrefab;
    
    private void OnCollisionEnter(Collision collision)
    {
        FindObjectOfType<AudioManager>().Play("targetHit");
        GameManager.Instance.TargetHit(this.name, collision, ingredientPrefab);
    }

    

    
}

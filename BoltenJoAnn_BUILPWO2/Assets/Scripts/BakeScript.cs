using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BakeScript : MonoBehaviour {


    private int eggSizeTemp;
    private int eggSize
    {
        set { print(eggSizeTemp); print(value); eggSizeTemp = value; }
        get { return eggSizeTemp; }
    }
    private int milkSize;
    private int breadSize;

    private int correctEggValue = 4;
    private int correctMilkValue = 20;
    private int correctBreadValue = 6;

    public GameObject cake;
    public GameObject blob;
    public Transform pos;
    private GameObject winCake;
    private GameObject loseCake;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Egg Ingredient")
        {
            //assign value of egg to private int
            ValuesScript eggValue = other.GetComponent<ValuesScript>();
            eggSize = eggValue.value;
            Debug.Log("Egg size is");
            Debug.Log(eggSize);
        }
        //else if (other.tag == "Milk Ingredient")
        //{
        //    ValuesScript milkValue = other.GetComponent<ValuesScript>();
        //    eggSize = milkValue.value;
        //    Debug.Log("Milk size is");
        //    Debug.Log(milkSize);
        //}
        //else if (other.tag == "Bread Ingredient")
        //{
        //    ValuesScript breadValue = other.GetComponent<ValuesScript>();
        //    eggSize = breadValue.value;
        //    Debug.Log("Bread size is");
        //    Debug.Log(breadSize);
        //}
    }

    public void BakeCake()
    {
        Debug.Log(eggSize);
        
        //check ingredients in Trigger
        if(eggSize == correctEggValue) //&& milkSize == correctMilkValue && breadSize == correctBreadValue)
        {
            //Instantiate a Cake
            winCake = (GameObject)Instantiate(cake, pos);

            //Show You Win text
            //play winning audio
            EndGame();
        }
        else
        {
            //Instantiate Black Blob
            loseCake = (GameObject)Instantiate(blob, pos);
            //Show you lose text
            //play losing audio
            EndGame();
        }

    }

    void EndGame()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Player pl = player.GetComponent<Player>();
        
        pl.alive = false;
        Time.timeScale = 0;
        //Show Start Menu or Try again Button
    }
}

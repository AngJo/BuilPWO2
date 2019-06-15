using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTipTrigger : MonoBehaviour {

    public GameObject textbox;
    private Transform targetSwitch;
    public float speed = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        
        if (this.transform.parent.tag == "ResetButton")
        {
            textbox.GetComponent<TextMeshProUGUI>().text = "Press E to Reset.";
            FindObjectOfType<AudioManager>().Play("Nameofsoundclip");

        }
        else if (this.transform.parent.tag == "DropButton")
        {
            textbox.GetComponent<TextMeshProUGUI>().text = "Press E to Drop.";
        }
        else
        {
            textbox.GetComponent<TextMeshProUGUI>().text = "";
        }

        textbox.SetActive(true);

    }

    private void OnTriggerExit(Collider other)
    {
        textbox.SetActive(false);
    }
}

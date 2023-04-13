using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public Text positionText;
    public float x, y, z;
    private SaveManager saveManager;

    

    // Start is called before the first frame update
    void Start()
    {
        saveManager = GameObject.Find("Player").GetComponent<SaveManager>();
        SaveData();
    }

    public void ShowData()
    {
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;

        PlayerPrefs.SetFloat("x" ,x);
        PlayerPrefs.SetFloat("y" ,y);
        PlayerPrefs.SetFloat("z" ,z);

        positionText.text = "Position: " +PlayerPrefs.GetFloat("x").ToString() + "x " + PlayerPrefs.GetFloat("y").ToString() + "y " + PlayerPrefs.GetFloat("z").ToString() +"z ";

    }

    public void SaveData()
    {
        x = PlayerPrefs.GetFloat("x");
        y = PlayerPrefs.GetFloat("y");
        z = PlayerPrefs.GetFloat("z");

        Vector3 LoadPosition = new Vector3(x,y,z);
        transform.position = LoadPosition;
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteKey("x");
        PlayerPrefs.DeleteKey("y");
        PlayerPrefs.DeleteKey("z");

        PlayerPrefs.DeleteAll();

    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Checkpoint"))
        {
            ShowData();
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public GameObject inventoryUI;
    public GameObject cam;
    public List<GameObject> abilities;
    public List<bool> abilitiesEnabled = new List<bool>();
    private int selectedAbility = 0;

    void Update()
    {
        // Check for number key press to select a prefab
        for (int i = 0; i < abilities.Count; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                if (abilitiesEnabled[i])
                {
                    selectedAbility = i;
                    Debug.Log("Using Ability " + (i + 1));
                    useAbility();
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            //useAbility();
        }
        
    }

    public void useAbility()
    {
        if (!inventoryUI.activeSelf)
        {
            Debug.Log("UI Check");
            Vector3 rayOrigin = transform.position + transform.forward * 10 + transform.up * 2;


            Vector3 rayDirection = -transform.up;

            Ray ray = new Ray(rayOrigin, rayDirection);
            RaycastHit hit;

            // Check if the ray hits an object
            if (Physics.Raycast(ray, out hit))
            {
                GameObject obj = new GameObject();
                if (selectedAbility == 1 || selectedAbility == 2)
                {
                    obj = Instantiate(abilities[selectedAbility], hit.point + transform.up * 1, Quaternion.identity);
                }
                else
                {
                    obj = Instantiate(abilities[selectedAbility], hit.point, Quaternion.identity);
                }
                StartCoroutine(timerCoroutine(obj));

            }
        }
    }

    public void EnableBool1()
    {
        abilitiesEnabled[0] = true;
    }

    public void EnableBool2()
    {
        abilitiesEnabled[1] = true;
    }

    public void EnableBool3()
    {
        abilitiesEnabled[2] = true;
    }

    public void EnableBool4()
    {
        abilitiesEnabled[3] = true;
    }

    public void EnableBool5()
    {
        abilitiesEnabled[4] = true;
    }


    private IEnumerator timerCoroutine(GameObject obj)
    {

        yield return new WaitForSeconds(2f);
        Destroy(obj);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Shield : MonoBehaviour, IEquipable
{
    GameObject Anchor;
    public void Equip()
    {
        Anchor = GameObject.Find("ShieldHolder");
        if (Anchor.transform.childCount > 0)
        {
            Transform childTransform = Anchor.transform.GetChild(0);
            GameObject childObject = childTransform.gameObject;
            Destroy(childObject);
        }
        
        transform.parent = Anchor.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

    }
    public void UnEquip()
    {

        Destroy(this.gameObject);

    }
}

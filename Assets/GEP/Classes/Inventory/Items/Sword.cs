using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IEquipable
{
    GameObject Anchor;
    public void Equip()
    {
        Anchor = GameObject.Find("SwordHolder");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPlatform : Platform {

    public override void AttachItem(GameObject itemPrefab)
    {  
        GameObject item = Instantiate(itemPrefab);

        Item itemScript = item.GetComponent<Item>();
        if (itemScript != null)
        {
            float itemWidth = itemScript.Width;
            float itemHeight = itemScript.Height;
            float itemPivotX = itemScript.PivotX;
            float itemPivotY = itemScript.PivotY;
            float platformHalfHeight = 0.15f;

            float newX = transform.position.x + itemWidth * (itemPivotX - 0.5f);
            float newY = transform.position.y + platformHalfHeight + itemHeight * itemPivotY - 0.08f;
            Vector3 itemPos = new Vector3(newX, newY, transform.position.z);
            item.transform.position = itemPos;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : CollectableController {

    public Sprite emptyChest;
    public int pesosAmount = 5;

    protected override void OnCollect() {
        // base.OnCollect();
        // Debug.Log("collected " + collected);
        if (!collected) {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            Debug.Log("Grand " + pesosAmount + " pesos");
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : CollidableController {

    //Logic
    protected bool collected;

    protected override void OnCollide(Collider2D coll) {
        // base.OnCollide(coll);
        // Debug.Log("OnCollide CollectableController");
        if (coll.name == "Player") {
            OnCollect();
        }
    }

    protected virtual void OnCollect() {
        collected = true;
    }
}

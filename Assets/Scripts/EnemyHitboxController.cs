using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitboxController : CollidableController {

    // Damage
    public int damage = 1;
    public float pushForce = 3;

    protected override void OnCollide(Collider2D coll) {
        // base.OnCollide(coll);

        if (coll.tag == "Fighter" && coll.name == "Player") {
            
            // Create a Damage objet, before sending it o the player
            DamageController dmg = new DamageController() {
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("ReceiveDamage", dmg);

            // Debug.Log("OnCollide " + coll.name);
        }
    }
}

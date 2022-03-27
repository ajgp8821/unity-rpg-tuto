using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : CollidableController {

    // Damage struct
    public int damagePoint = 1;
    public float pushForce = 4.0f;

    // Upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    // Swing
    private float coolDown = 0.5f;
    private float lastSwing;

    protected override void Start() {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update() {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (Time.time - lastSwing < coolDown) {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll) {
        // base.OnCollide(coll);
        if (coll.tag == "Fighter") {
            if (coll.name == "Player") {
                return;
            }

            // Create a new Damage object, then we'll send it to the fighter we've hit
            DamageController dmg = new DamageController() {
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("ReceiveDamage", dmg);

            Debug.Log("OnCollide " + coll.name);
        }
    }

    private void Swing() {
        Debug.Log("Swing");
    }
}

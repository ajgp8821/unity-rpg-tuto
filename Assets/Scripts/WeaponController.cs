using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : CollidableController {

    // Damage struct
    public int[] damagePoint = { 1, 2, 3, 4, 5, 6, 7, 8 };
    public float[] pushForce = { 2.0f, 2.2f, 2.5f, 2.8f, 3f, 3.2f, 3.6f, 4f };

    // Upgrade
    public int weaponLevel = 0;
    public SpriteRenderer spriteRenderer;

    // Swing
    private Animator anim;
    private float coolDown = 0.5f;
    private float lastSwing;

    /*private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }*/

    protected override void Start() {
        base.Start();
        // spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected override void Update() {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (Time.time - lastSwing > coolDown) {
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
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };

            coll.SendMessage("ReceiveDamage", dmg);

            Debug.Log("OnCollide " + coll.name);
        }
    }

    private void Swing() {
        // Debug.Log("Swing");
        anim.SetTrigger("Swing");
    }

    public void UpgradeWeapon() {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

        // Change stats %%
    }

    public void SetWeaponLevel(int level) {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
}

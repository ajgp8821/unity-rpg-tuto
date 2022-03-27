using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : CollectableController {

    public string[] sceneNames;

    protected override void OnCollide(Collider2D coll) {

        // GameManager.instance.ShowText();

        // base.OnCollide(coll);
        if (coll.name == "Player") {
            // Teleport the player
            GameManager.instance.SaveState();
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
            SceneManager.LoadScene(sceneName);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {
    public float tileSizeX, tileSizeY;

    private Vector2 playerStartPosition;
    private Vector2 startPosition;
    private PlayerController player;

    void Start()
    {
        player = PlayerController.GetPlayer();
        playerStartPosition = player.transform.position;

        startPosition = transform.position;
    }

    void Update() {
        float xOffset = Mathf.Repeat(player.transform.position.x * 0.1f, 2* tileSizeX);
        float yOffset = (player.transform.position.y - playerStartPosition.y) * 0.9f + startPosition.y;
        transform.position = player.transform.position + Vector3.left * (xOffset - tileSizeX) + Vector3.up * (yOffset - player.transform.position.y);
    }
}

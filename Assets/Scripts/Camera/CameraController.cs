using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    [HideInInspector] public Camera cam; // The camera component
    [HideInInspector] public float camBuffer = 1f;
    public LayerMask verticalWallMask;  // The layer mask for the top wall and bottom wall of a level
    public LayerMask horizontalWallMask;  // The layer mask for the left wall and right wall of a level

    void Start(){
        cam = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        float camSizeY = cam.orthographicSize;
        float camSizeX = cam.orthographicSize * cam.aspect;

        // Use raycasting to check for left and right walls
        if (Physics2D.Raycast(player.position, Vector2.left, camSizeX * camBuffer, horizontalWallMask) || Physics2D.Raycast(player.position, Vector2.right, camSizeX * camBuffer, horizontalWallMask))
        {
            // Continue following y position of player, unless player hits top or bottom wall
            if (Physics2D.Raycast(player.position, Vector2.up, camSizeY * camBuffer, verticalWallMask) || Physics2D.Raycast(player.position, Vector2.down, camSizeY * camBuffer, verticalWallMask)){
                cam.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
            else {
                cam.transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
            }
        }
        // Use raycasting to check for top and bottom walls
        else if (Physics2D.Raycast(player.position, Vector2.up, camSizeY * camBuffer, verticalWallMask) || Physics2D.Raycast(player.position, Vector2.down, camSizeY * camBuffer, verticalWallMask))
        {
            // Continue following x position of player, unless player hits left or right wall
            if (Physics2D.Raycast(player.position, Vector2.right, camSizeX * camBuffer, horizontalWallMask) || Physics2D.Raycast(player.position, Vector2.left, camSizeX * camBuffer, horizontalWallMask)){
                cam.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
            else {
                cam.transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
            }
        }
        else
        {
            // Smoothly follow the target if cam is not hitting any walls
            cam.transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        }
    }
}

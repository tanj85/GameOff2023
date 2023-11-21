using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    [HideInInspector] public CinemachineVirtualCamera vcam; // The vcam component
    public float smoothTime = 0.3f;
    [HideInInspector] public float vcamBufferX = 1.6f;
    [HideInInspector] public float vcamBufferY = 0.95f;
    public LayerMask verticalWallMask;  // The layer mask for the top wall and bottom wall of a level
    public LayerMask horizontalWallMask;  // The layer mask for the left wall and right wall of a level

    void Start(){
        vcam = GetComponent<CinemachineVirtualCamera>();
        vcam.LookAt = player;
        vcam.Follow = player;
    }

    void FixedUpdate()
    {
        float vcamOrthoSize = vcam.m_Lens.OrthographicSize;

        // Use raycasting to check for left and right walls
        if (Physics2D.Raycast(player.position, Vector2.left, vcamOrthoSize * vcamBufferX, horizontalWallMask) || Physics2D.Raycast(player.position, Vector2.right, vcamOrthoSize * vcamBufferX, horizontalWallMask))
        {
            // Adjust the camera position to be just before the obstacle
            vcam.LookAt = null;
            vcam.Follow = null;
            // Continue following y position of player, unless player hits top or bottom wall
            if (Physics2D.Raycast(player.position, Vector2.up, vcamOrthoSize * vcamBufferY, verticalWallMask) || Physics2D.Raycast(player.position, Vector2.down, vcamOrthoSize * vcamBufferY, verticalWallMask)){
                vcam.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
            else {
                vcam.transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
            }
        }
        // Use raycasting to check for top and bottom walls
        else if (Physics2D.Raycast(player.position, Vector2.up, vcamOrthoSize * vcamBufferY, verticalWallMask) || Physics2D.Raycast(player.position, Vector2.down, vcamOrthoSize * vcamBufferY, verticalWallMask))
        {
            // Adjust the camera position to be just before the obstacle
            vcam.LookAt = null;
            vcam.Follow = null;
            // Continue following x position of player, unless player hits left or right wall
            if (Physics2D.Raycast(player.position, Vector2.right, vcamOrthoSize * vcamBufferX, horizontalWallMask) || Physics2D.Raycast(player.position, Vector2.left, vcamOrthoSize * vcamBufferX, horizontalWallMask)){
                vcam.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
            else {
                vcam.transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            }
        }
        else
        {
            // Smoothly follow the target if vcam is not hitting any walls
            vcam.LookAt = player;
            vcam.Follow = player;
        }
    }
}

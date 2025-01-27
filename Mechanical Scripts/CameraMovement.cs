using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraMovement : MonoBehaviour
{
    // inspired by Shack man youtube
    public Camera cam;
    public Transform cameraTransform;
    public bool allowPlayerMoveControl;
    public int zoomAmount, minSize, maxSize;                        // for zoom
    public int moveAmount, edgeHorizontalDelta, edgeVerticalDelta;  // for pann
    public float mapMinX, mapMaxX, mapMinY, mapMaxY;                // for pann
    public float panForActionLimitPercentHorizontal, panForActionLimitPercentVertical;      // defines the veiwing box when using the PanTo function
    public int autoMoveAmount;
    private Vector3 mousePositionWorld;

    // Start is called before the first frame update
    public void OnLevelStart()
    {
        GameManager gameManager = GameManager.Instance;
        float cellSizeX = gameManager.tileMapGrid.cellSize.x;
        float cellSizeY = gameManager.tileMapGrid.cellSize.y;
        int mapCellsAcross = gameManager.Level.tileMap.size.x;
        int mapCellsTall = gameManager.Level.tileMap.size.y;

        mapMinX = -3;
        mapMaxX = cellSizeX*mapCellsAcross+2;
        mapMinY = -3;
        mapMaxY = cellSizeY*mapCellsTall+2;

        // CONSIDER setting the max camera size dynamicly based on level size
    }

    // Update is called once per frame
    void Update()
    {
        ZoomCamera();
        if (allowPlayerMoveControl)
        {
            MoveCamera();
        }
        
    }

    private void MoveCamera()
    {
        if (Input.mousePosition.x >= Screen.width*0.95)
        {
            cameraTransform.position = ClampCamera(cameraTransform.position + Time.deltaTime * moveAmount*Vector3.right) ;
        } 
        if (Input.mousePosition.x <= Screen.width * 0.05)
        {
            cameraTransform.position = ClampCamera(cameraTransform.position + Vector3.left * Time.deltaTime * moveAmount);
        } 
        if (Input.mousePosition.y >= Screen.height*0.95)
        {
            cameraTransform.position = ClampCamera(cameraTransform.position + Vector3.up * Time.deltaTime * moveAmount);
        } 
        if (Input.mousePosition.y <= Screen.height*0.05)
        {
            cameraTransform.position = ClampCamera(cameraTransform.position + Vector3.down * Time.deltaTime * moveAmount);
        }
    }

    private void ZoomCamera()
    {
        cam.orthographicSize = Math.Clamp(cam.orthographicSize- Input.mouseScrollDelta.y * zoomAmount,minSize,maxSize);
        //cam.transform.position = ClampCamera(cam.transform.position);
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        // half camera height
        float camHeight = cam.orthographicSize;
        // half camera width
        float camWidth = cam.orthographicSize* cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }

    public IEnumerator PanTo(Vector3Int startLocationInt, Vector3Int endLocationInt)
    {
        Vector3 startLocation = GameManager.Instance.tileMapGrid.CellToWorld(startLocationInt);
        Vector3 endLocation = GameManager.Instance.tileMapGrid.CellToWorld(endLocationInt);

        //Vector3 adjustedLoaction =new Vector3((endLocation.x + startLocation.x)/2, (endLocation.y + startLocation.y) / 2,-10);


        Vector3 adjustedLocation = AdjustPanToLocation(transform.position, startLocation);
        adjustedLocation = AdjustPanToLocation(adjustedLocation, endLocation);

        bool IsAnimationFin = false;


        while (!IsAnimationFin)
        {
            transform.position = Vector3.Lerp(transform.position, adjustedLocation, autoMoveAmount*Time.deltaTime);
            if (Mathf.Abs(transform.position.x - adjustedLocation.x) < 0.1 && Mathf.Abs(transform.position.y - adjustedLocation.y) <0.1 )       // if location is apprx correct
            {
                IsAnimationFin = true;
            }
            yield return null;
        }


        yield break;
    }

    private Vector3 AdjustPanToLocation(Vector3 startLocation, Vector3 endLocation)
    {
        float targetPositionX;
        float targetPositionY;

        float horizontalDistance = endLocation.x - startLocation.x;   // -ve if target to the left, +ve if to the right
        float verticalDistance = endLocation.y - startLocation.y;     // -ve if target bellow, +ve if above

        // distance from centre of cam to vertical egde of viewing box (right)
        float panAllowanceHorizontal = cam.orthographicSize * cam.aspect * panForActionLimitPercentHorizontal;
        // distance from centre of cam to horizontal egde of viewing box (upwards)
        float panAllowanceVertical = cam.orthographicSize * panForActionLimitPercentVertical;

        if(MathF.Abs(horizontalDistance) < panAllowanceHorizontal)
        {
            targetPositionX = startLocation.x;     // target location is within horizontal tolerence so don't pan horizontally
        } else if(horizontalDistance < 0)
        {
            targetPositionX = startLocation.x + horizontalDistance + panAllowanceHorizontal;       // current position plus distance between cam and target adjusted for viewbox size
        }
        else
        {
            targetPositionX = startLocation.x + horizontalDistance - panAllowanceHorizontal;       // allowane changes sign because it is always declared +ve, whereas horizontal distance con be either sign
        }

        if (MathF.Abs(verticalDistance) < panAllowanceVertical)
        {
            targetPositionY = startLocation.y;     // target location is within vertical tolerence so don't pan horizontally
        }
        else if (verticalDistance < 0)
        {
            targetPositionY = startLocation.y + verticalDistance + panAllowanceVertical;
        }
        else
        {
            targetPositionY = startLocation.y + verticalDistance - panAllowanceVertical;
        }


            return ClampCamera(new Vector3(targetPositionX, targetPositionY, -10));
    }

    public void EnableCameraPan()
    {
        allowPlayerMoveControl = true;
    }

    public void DisableCameraPan()
    {
        allowPlayerMoveControl = false;
    }
}

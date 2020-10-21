using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMoveManager : MonoBehaviour
{
    private void Start()
    {
        EventAggregator.OnClickBlockEvent.Subscribe(MoveSelector);
    }
    public void MoveSelector(GameObject clickedBlock)
    {
        var newCoordinates = GetMovingCoordinate(clickedBlock);
        var borderCross = CheckBorder(newCoordinates);
        if (borderCross)
        {
            GameObject neighborBlock = EventAggregator.SelectBlockRequestEvent.Publish(newCoordinates);
            if (neighborBlock != null)
            {
                SwapBlock(clickedBlock, neighborBlock);
            }
            else
            {
                MoveBlock(clickedBlock, newCoordinates);
            }
        }
        else
        {
            EventAggregator.OnEndLockClickButton.Publish(false);
        }
    }
    private void SwapBlock(GameObject clickedBlock, GameObject neighborBlock)
    {
        SwaperRun(clickedBlock, neighborBlock, (float)1);
    }
    private void MoveBlock(GameObject clickedBlock, Vector3 newCoordinates)
    {
        var oldCoordinates = clickedBlock.transform.position;
        MoverRun(clickedBlock, newCoordinates, (float)2);
    }
    private bool CheckBorder(Vector3 coordinates)
    {
        var x = coordinates.x;
        var y = coordinates.y;
        var conditionFirst = System.Math.Abs(x) <=2 ? true : false;
        var conditionSecond = System.Math.Abs(y) <= 2 ? true : false;
        return conditionFirst && conditionSecond;
    }
    public void MoverRun(GameObject clickedBlock, Vector3 newCoordinates, float time)
    {
        var moveCoroutine = Mover(clickedBlock, newCoordinates, time);
        StartCoroutine(moveCoroutine);
    }
    private IEnumerator Mover(GameObject clickedBlock, Vector3 newCoordinates, float time ) 
    {
        var oldCoordinates = clickedBlock.transform.position;
        for (float i = 0; i <= 1*time; i+=Time.deltaTime*5)
        {
            clickedBlock.transform.position =Vector3.Lerp(clickedBlock.transform.position, newCoordinates, (i / time));  //position = //vector;
            yield return 0;
        }
        clickedBlock.transform.position = newCoordinates;
        EventAggregator.OnUpdatingBlockEvent.Publish(oldCoordinates, clickedBlock);
        EventAggregator.OnUpdatedBlockEvent.Publish();
        EventAggregator.OnEndLockClickButton.Publish(false);
    }
    public void SwaperRun(GameObject clickedBlock, GameObject neighborBlock, float time)
    {
        var moveCoroutine = Swaper(clickedBlock, neighborBlock, time);
        StartCoroutine(moveCoroutine);
    }
    private IEnumerator Swaper(GameObject clickedBlock, GameObject neighborBlock, float time )
    {
        var oldCoordinatesClickedBlock = clickedBlock.transform.position;
        var oldCoordinatesNeighborBlock = neighborBlock.transform.position;
        clickedBlock.transform.position = new Vector3(oldCoordinatesNeighborBlock.x, oldCoordinatesNeighborBlock.y, 2);
        
        for (float i = 0; i <= 1*time; i+=Time.deltaTime*5)
        {
            clickedBlock.transform.position = Vector3.Lerp(oldCoordinatesClickedBlock, oldCoordinatesNeighborBlock, (i / time));
            neighborBlock.transform.position = Vector3.Lerp(oldCoordinatesNeighborBlock, oldCoordinatesClickedBlock, (i / time));
            yield return 0;
        }
        neighborBlock.transform.position = oldCoordinatesClickedBlock;
        clickedBlock.transform.position = oldCoordinatesNeighborBlock;
        EventAggregator.OnUpdatingBlockEvent.Publish(oldCoordinatesClickedBlock, clickedBlock);
        EventAggregator.OnUpdatingBlockEvent.Publish(oldCoordinatesNeighborBlock, neighborBlock);
        EventAggregator.OnUpdatedBlockEvent.Publish();
        EventAggregator.OnEndLockClickButton.Publish(false);
        //EventAggregator.OnSwapBlockEvent.Publish(oldCoordinatesClickedBlock, clickedBlock, oldCoordinatesNeighborBlock, neighborBlock);
    }
    public Vector3 GetMovingCoordinate(GameObject clickedBlock)
    {
        Vector3 coordinateMoving = new Vector3();
        switch (clickedBlock.tag)
        {
            case "Up":
                coordinateMoving = MoveUp(clickedBlock);
                break;
            case "Down":
                coordinateMoving = MoveDown(clickedBlock);
                break;
            case "Left":
                coordinateMoving = MoveLeft(clickedBlock);
                break;
            case "Right":
                coordinateMoving = MoveRight(clickedBlock);
                break;
            case "UpRight":
                coordinateMoving = MoveUpRight(clickedBlock);
                break;
            case "DownRight":
                coordinateMoving = MoveDownRight(clickedBlock);
                break;
            case "UpLeft":
                coordinateMoving = MoveUpLeft(clickedBlock);
                break;
            case "DownLeft":
                coordinateMoving = MoveDownLeft(clickedBlock);
                break;
        }
        return coordinateMoving;
    }
    private Vector3 MoveDown(GameObject gameObject)
    {
        Vector3 position;
        position = gameObject.transform.position;
        position.y -= 1;
        return position;
    }
    private Vector3 MoveUp(GameObject gameObject)
    {
        Vector3 position;
        position = gameObject.transform.position;
        position.y += 1;
        return position;
    }
    private Vector3 MoveLeft(GameObject gameObject)
    {
        Vector3 position;
        position = gameObject.transform.position;
        position.x -= 1;
        return position;
    }
    private Vector3 MoveRight(GameObject gameObject)
    {
        Vector3 position;
        position = gameObject.transform.position;
        position.x += 1;
        return position;
    }
    private Vector3 MoveUpRight(GameObject gameObject)
    {
        Vector3 position;
        position = gameObject.transform.position;
        position.x += 1;
        position.y += 1;
        return position;
    }
    private Vector3 MoveDownRight(GameObject gameObject)
    {
        Vector3 position;
        position = gameObject.transform.position;
        position.x += 1;
        position.y -= 1;
        return position;
    }
    private Vector3 MoveUpLeft(GameObject gameObject)
    {
        Vector3 position;
        position = gameObject.transform.position;
        position.x -= 1;
        position.y += 1;
        return position;
    }
    private Vector3 MoveDownLeft(GameObject gameObject)
    {
        Vector3 position;
        position = gameObject.transform.position;
        position.x -= 1;
        position.y -= 1;
        return position;
    }
}

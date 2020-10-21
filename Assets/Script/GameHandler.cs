using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
   
    //public GameObject cube;
    public GameObject Up;
    public GameObject Down;
    public GameObject Left;
    public GameObject Right;
    public GameObject UpLeft;
    public GameObject DownLeft;
    public GameObject UpRight;
    public GameObject DownRight;
    bool isLock = false;
    void Start()
    {
        //EventAggregator.onDataBaseCreated.Subscribe(GenerateBlocksForBords);
        EventAggregator.OnNewGameClick.Subscribe(GenerateBlocksForBords);
        EventAggregator.OnEndLockClickButton.Subscribe(SwitchIsLockOption);
        EventAggregator.GetIsLockState.Subscribe(GetIsLockState);
        EventAggregator.OnEndGameChecking.Subscribe(CheckForGameOver);
    }
    private void SwitchIsLockOption(bool lockState)
    {
        if (isLock!=lockState)
        {
            isLock = lockState;
            if (!lockState)
            {
                EventAggregator.OnUpdatedBlockEvent.Publish();
            }
        }
    }
    private bool GetIsLockState()
    {
        return isLock;
    }
    private void GenerateBlocksForBords()
    {
        var x = 2;
        var y = 2;
        var borderScale = 4 * x * y + (x * 2 + y * 2 + 1);
        int border = int.Parse((borderScale * 0.6).ToString());
        while (EventAggregator.getBlocksRequestEvent.Publish().Count - border < 0)
        {
            GenerateRandomBlock(2, 2);
            EventAggregator.OnUpdatedBlockEvent.Publish();
        }
    }
    private void CheckForGameOver()
    {
        var workingCopy = EventAggregator.getBlocksRequestEvent.Publish();
        if (workingCopy.Count <= 22)
        {
            for (int i = 0; i < 3; i++)
            {
                GenerateRandomBlock(2, 2);
                
            }
        }
        else
        {
            foreach (var item in workingCopy)
            {
                Destroy(item.Value);
                    
            }
            EventAggregator.OnCleaningDataBase.Publish();
        }
    }
    private void GenerateRandomBlock(int x, int y)
    {
        var endLoop = false;
        while (!endLoop)
        {
            var xNew = Random.Range(-x, x+1);
            var yNew = Random.Range(-x, x+1);
            var vector = new Vector3(xNew, yNew, 0);
            if (EventAggregator.SelectBlockRequestEvent.Publish(vector) == null)
            {
                GenerateBlock(vector);
                endLoop = true;
            }
        }
    }
    private void GenerateBlock(Vector3 vector)
    {
        var cube = GetNewBlock();
        var block = Instantiate(cube,vector, Quaternion.identity);
        EventAggregator.OnAddingBlockEvent.Publish(block);
    }
    private GameObject GetNewBlock()
    {
        GameObject[] array = { Up, Down, Left, Right, UpLeft, UpRight,DownLeft,DownRight };
        var direction = Random.Range(0, 8);//random.Next(0, 4);
        return array[direction];
    }
    
}

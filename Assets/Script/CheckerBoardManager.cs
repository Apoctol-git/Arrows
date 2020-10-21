using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerBoardManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventAggregator.OnUpdatedBlockEvent.Subscribe(chechNeighbordBlock);
    }
    private void chechNeighbordBlock()
    {
        var dataBase = EventAggregator.getBlocksRequestEvent.Publish();
        var keysAndIsNeighborExistSimilar = GetKeysAndIsNeighborExistSimilar(dataBase, 2, 2);
        DeleteSelectedObject(keysAndIsNeighborExistSimilar);
    }
    private void DeleteSelectedObject(Dictionary<Vector3, GameObject> selectedBlocks)
    {
        foreach (var item in selectedBlocks)
        {
            Destroy(item.Value);
            EventAggregator.OnDeletingBlockEvent.Publish(item.Key);
        }
        if (EventAggregator.GetIsLockState.Publish())
        {
            EventAggregator.OnEndGameChecking.Publish();
        }
        
    }
    private Dictionary<Vector3, GameObject> GetKeysAndIsNeighborExistSimilar(Dictionary<Vector3, GameObject> dataBase, int x, int y)
    {
        var result = new Dictionary<Vector3, GameObject>();
        for (int i = -x; i <= x; i++)
        {
            for (int j = -y; j <= y; j++)
            {
                var currentKey = new Vector3(i, j);
                if (dataBase.ContainsKey(currentKey))
                {
                    var similarObjectNeighbord = SimilarObjectNeighbord(dataBase, currentKey);
                    if (similarObjectNeighbord!=null)
                    {
                        if (!result.ContainsKey(currentKey))
                        {
                            result.Add(currentKey, dataBase[currentKey]);
                        }
                        foreach (var item in similarObjectNeighbord)
                        {
                            if (item!=null)
                            {
                                if (!result.ContainsKey(item.transform.position))
                                {
                                    result.Add(item.transform.position, item);
                                }    
                            }
                                           
                        }
                    }
                }
            }
        }
        return result;
    }
    private GameObject[] SimilarObjectNeighbord(Dictionary<Vector3, GameObject> dataBase, Vector3 currentVector)
    {
        GameObject up = null;
        GameObject down = null;
        GameObject left = null;
        GameObject right = null;
        
        if (dataBase.ContainsKey(new Vector3(currentVector.x, currentVector.y+1)))
        {
            var vector = new Vector3(currentVector.x, currentVector.y + 1);
            if (dataBase[currentVector].tag == dataBase[vector].tag)
            {
                up = dataBase[vector];
            }
        }
        if (dataBase.ContainsKey(new Vector3(currentVector.x, currentVector.y-1)))
        {
            var vector = new Vector3(currentVector.x, currentVector.y - 1);
            if (dataBase[currentVector].tag == dataBase[vector].tag)
            {
                down = dataBase[vector];
            }
        }
        if (dataBase.ContainsKey(new Vector3(currentVector.x-1, currentVector.y)))
        {
            var vector = new Vector3(currentVector.x - 1, currentVector.y);
            if (dataBase[currentVector].tag == dataBase[vector].tag)
            {
                left = dataBase[vector];
            }
        }
        if (dataBase.ContainsKey(new Vector3(currentVector.x+1, currentVector.y)))
        {
            var vector = new Vector3(currentVector.x + 1, currentVector.y);
            if (dataBase[currentVector].tag == dataBase[vector].tag)
            {
                right = dataBase[vector];
            }
        }
        if (up != null || down != null|| left!=null || right!=null)
        {
            GameObject[] result = { up, down, left, right };
            return result;
        }
        else
        {
            return null;
        }
    }
}

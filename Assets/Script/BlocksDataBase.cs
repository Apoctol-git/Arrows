using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksDataBase : MonoBehaviour
{
    private Dictionary<Vector3,GameObject> blocks = new Dictionary<Vector3, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        EventAggregator.OnAddingBlockEvent.Subscribe(AddBlock);
        EventAggregator.OnUpdatingBlockEvent.Subscribe(UpdateBlock);
        EventAggregator.OnDeletingBlockEvent.Subscribe(DeleteBlock);
        EventAggregator.SelectBlockRequestEvent.Subscribe(SelectBlock);
        EventAggregator.getBlocksRequestEvent.Subscribe(GetBlocks);
        EventAggregator.OnDataBaseCreated.Publish();
        EventAggregator.OnCleaningDataBase.Subscribe(CleanDataBase);
    }
    private void AddBlock(GameObject block)
    {
        blocks.Add(block.transform.position,block);
    }
    
    private void UpdateBlock(Vector3 oldCoordinates, GameObject block)
    {
        var newCoordinates = block.transform.position;
        if (blocks.ContainsKey(newCoordinates))
        {
            blocks[newCoordinates] = block;
        }
        else
        {
            DeleteBlock(oldCoordinates);
            AddBlock(block);
        }
    }
 
    private void DeleteBlock(Vector3 coordinates)
    {
        if (SelectBlock(coordinates)!=null)
        {
            blocks.Remove(coordinates);
        }
        
    }
     private GameObject SelectBlock(Vector3 coordinates)
    {
        if (blocks.ContainsKey(coordinates))
        {
            return blocks[coordinates];
        }
        else
        {
            return null;
        }
    }

    private Vector3 SelectKeyBlock(Vector3 coordinates)
    {
        Vector3 result = Vector3.back; ;
        foreach (var item in blocks)
        {
            if (item.Equals(coordinates))
            {
                result = item.Key;
            }
        }
        return result;
    }
    private Dictionary<Vector3, GameObject> GetBlocks()
    {
        return blocks;
    }
    private void CleanDataBase()
    {
        blocks.Clear();
    }
    private bool Equals(Vector3 other)
    {
        var firstCondition = transform.position.x == other.x ? true : false;
        var secondCondition = transform.position.y == other.y ? true : false;
        return firstCondition && secondCondition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeController : MonoBehaviour
{
    private void OnMouseDown()
    {
        var condition = EventAggregator.GetIsLockState.Publish();
        if (!condition)
        {
            EventAggregator.OnEndLockClickButton.Publish(true);
            EventAggregator.OnClickBlockEvent.Publish(gameObject);
        }
        
    }

}

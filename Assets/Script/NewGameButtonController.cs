using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameButtonController : MonoBehaviour
{
    private void OnMouseDown()
    {
        EventAggregator.OnNewGameClick.Publish();
    }
}

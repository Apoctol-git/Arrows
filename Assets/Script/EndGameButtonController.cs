using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameButtonController : MonoBehaviour
{
    private void OnMouseDown()
    {
        Application.Quit();
    }
}

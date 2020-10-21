using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PatreonButtonController : MonoBehaviour
{
    private void OnMouseDown()
    {
        string url = "patreon.com/casuallabs";
        Application.OpenURL(url);
    }
}

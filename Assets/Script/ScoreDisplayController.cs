using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplayController : MonoBehaviour
{
    int score;
    public GameObject zero;
    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;
    public GameObject five;
    public GameObject six;
    public GameObject seven;
    public GameObject eith;
    public GameObject nine;
    GameObject[] savedArray = null;
    // Start is called before the first frame update
    void Start()
    {
        OnRenderTable();
        EventAggregator.OnNewGameClick.Subscribe(OnRenderTable);
        EventAggregator.OnDeletingBlockEvent.Subscribe(ForPlusScorePoint);
    }
    private void OnRenderTable()
    {
        score = 0;
        UpdateScoreTable();
    }
    private void ForPlusScorePoint(Vector3 nuller)
    {
        score += 10;
        UpdateScoreTable();
    }
    private void UpdateScoreTable()
    {
        var arrayGO = FormScorePointTitle();
        if (savedArray!=null)
        {
            foreach (var item in savedArray)
            {
                Destroy(item);
            }
        }
        var x = 3.5;
        var y = 3.5;
        GameObject[] arraySetter = { null, null, null, null, null, null };
        for (int i = 0; i < arrayGO.Length ; i++)
        {
            x = x - 0.5;
            arraySetter[i]=Instantiate(arrayGO[i], new Vector3((float)x, (float)y), Quaternion.identity);
        }
        savedArray = arraySetter;
    }
    private GameObject[] FormScorePointTitle()
    {
        GameObject[] result = { null, null, null, null, null, null };
        for (int i = 1; i <= 6; i++)
        {
            var currentDimention = System.Math.Pow(10, i);
            var currentSubDimention = System.Math.Pow(10, i-1);
            int currentSwither = (int) (score % currentDimention / currentSubDimention);
            result[i-1]=SwitherHandle(currentSwither);
        }
        return result;
    }
    private GameObject SwitherHandle(int swither)
    {
        GameObject result = null;
        switch (swither)
        {
            case 0:
                result = zero;
                break;
            case 1:
                result = one;
                break;
            case 2:
                result = two;
                break;
            case 3:
                result = three;
                break;
            case 4:
                result = four;
                break;
            case 5:
                result = five;
                break;
            case 6:
                result = six;
                break;
            case 7:
                result = seven;
                break;
            case 8:
                result = eith;
                break;
            case 9:
                result = nine;
                break;
        }
        return result;
    }


}

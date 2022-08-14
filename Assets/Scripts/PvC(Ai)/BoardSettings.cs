using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardSettings : MonoBehaviour
{
    public  List<int> EmptyBoardPoints = new List<int>();
   

    private int index1,idx;
  
    private void Start()
    {
        AddtoPositionList();
       SetBoard();
    }

    public void AddtoPositionList()
    {
        EmptyBoardPoints.Clear();
        EmptyBoardPoints.Add(11);
        EmptyBoardPoints.Add(12);
        EmptyBoardPoints.Add(13);
        EmptyBoardPoints.Add(14);
        EmptyBoardPoints.Add(15);
        EmptyBoardPoints.Add(16);
        EmptyBoardPoints.Add(17);
        EmptyBoardPoints.Add(18);
        EmptyBoardPoints.Add(19);
    }

    public void removefromList()
    {
        EmptyBoardPoints.RemoveAt(5);
    }

    public void SetBoard()
    {
        Debug.Log("size of list 1=="+EmptyBoardPoints.Count);
        removefromList();
        Debug.Log("size of list after remove 1=="+EmptyBoardPoints.Count);
       // index1 = EmptyBoardPoints[Random.Range(1, EmptyBoardPoints.Count-1)];
       index1 = Random.Range(0, EmptyBoardPoints.Count - 1);
       // index1 = EmptyBoardPoints[Mathf.Clamp(Random.Range(0, EmptyBoardPoints.Count-1), 0, EmptyBoardPoints.Count-1)];
         idx = EmptyBoardPoints[index1];
        Debug.Log("index= " + index1.ToString());
        Debug.Log("value="+idx.ToString());
        
        Debug.Log("size of list 2 =="+EmptyBoardPoints.Count);
        removefromList();
        Debug.Log("size of list after remove 2=="+EmptyBoardPoints.Count);
        // index1 = EmptyBoardPoints[Random.Range(1, EmptyBoardPoints.Count-1)];
        index1 = Random.Range(0, EmptyBoardPoints.Count - 1);
        // index1 = EmptyBoardPoints[Mathf.Clamp(Random.Range(0, EmptyBoardPoints.Count-1), 0, EmptyBoardPoints.Count-1)];
         idx = EmptyBoardPoints[index1];
        Debug.Log("index= " + index1.ToString());
        Debug.Log("value="+idx.ToString());

      
        
    }
}
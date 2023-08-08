using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class OrderScrambler : MonoBehaviour
{
    [SerializeField] private List<Image> orderSingsList;
    [SerializeField] private List<PuzzleBalloonsGameItemsColors> colorsEnumList;
    [SerializeField] private List<Color> colorsList;

    public List<Color> ColorsList => colorsList;

    public List<PuzzleBalloonsGameItemsColors> ColorsEnumList => colorsEnumList;

    private void Start()
    {
        Shuffle(ColorsList,ColorsEnumList);
        for (int i = 0; i < orderSingsList.Capacity; i++)
        {
            orderSingsList[i].color = ColorsList[i];
        }
    }
    
    
    
    
    private void Shuffle(List<Color> inputList0,List<PuzzleBalloonsGameItemsColors> inputList1)
    {

        for (int i = 0; i < inputList1.Count - 1; i++)

        {

            var temp0 = inputList0[i];
            var temp1 = inputList1[i];

            int rand = Random.Range(i, inputList1.Count);
            inputList0[i] = inputList0[rand];
            inputList1[i] = inputList1[rand];
            inputList0[rand] = temp0;
            inputList1[rand] = temp1;

        }

    }
}

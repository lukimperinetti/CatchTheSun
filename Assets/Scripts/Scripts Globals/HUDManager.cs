using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    public void TravelDistance(float dist)
    {
        text.text = dist.ToString("F0") + "m"; //f0 = take 0 numbers after the comma
    }
}

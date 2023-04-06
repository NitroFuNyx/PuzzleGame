using UnityEngine;

public class SafeButton : MonoBehaviour
{
    [SerializeField] private string safeButtonNumber;

    public string SafeButtonNumber => safeButtonNumber;
}

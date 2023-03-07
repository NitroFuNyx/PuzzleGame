using UnityEngine;

[ExecuteAlways]
public class Splitter : MonoBehaviour
{
    [SerializeField] private Color textColor = Color.white;
    [SerializeField] private Color backgroundColor = Color.black;
    [SerializeField] private TextAnchor textAlignment = TextAnchor.MiddleCenter;

    public Color TextColor => textColor;
    public Color BackgroundColor => backgroundColor;
    public TextAnchor TextAlignment => textAlignment;

#if UNITY_EDITOR
    private void Update() => transform.DetachChildren();
#endif
}

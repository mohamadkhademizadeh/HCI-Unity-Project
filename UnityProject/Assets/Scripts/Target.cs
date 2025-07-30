using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private string targetId = "TargetA";
    [SerializeField] private Renderer rend;
    [SerializeField] private Color idleColor = Color.gray;
    [SerializeField] private Color highlightColor = Color.yellow;
    [SerializeField] private Color correctColor = Color.green;
    [SerializeField] private Color wrongColor = Color.red;

    public string TargetId => targetId;
    public bool IsHighlighted { get; private set; }

    void Awake() {
        if (rend == null) rend = GetComponentInChildren<Renderer>();
        SetColor(idleColor);
    }

    public void SetHighlighted(bool on) {
        IsHighlighted = on;
        SetColor(on ? highlightColor : idleColor);
    }

    public void Feedback(bool hit) {
        SetColor(hit ? correctColor : wrongColor);
        CancelInvoke(nameof(ResetColor));
        Invoke(nameof(ResetColor), 0.35f);
    }

    void ResetColor() => SetColor(IsHighlighted ? highlightColor : idleColor);

    void SetColor(Color c) {
        if (rend != null && rend.material.HasProperty("_Color"))
            rend.material.color = c;
    }
}

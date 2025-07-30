using UnityEngine;

public class GazeSelection : SelectionTechnique
{
    [SerializeField] private Camera hmdCamera;
    [SerializeField] private float maxDistance = 20f;
    [SerializeField] private float dwellTime = 0.6f;

    private bool enabledFlag;
    private float dwellCounter = 0f;
    private Target current;

    void Reset() { hmdCamera = Camera.main; }

    public override void EnableTechnique(bool enable) {
        enabledFlag = enable;
        dwellCounter = 0f;
        if (current) current.SetHighlighted(false);
        current = null;
        gameObject.SetActive(enable);
    }

    public override Target GetAimedTarget() {
        if (!enabledFlag || hmdCamera == null) return null;
        Ray ray = new Ray(hmdCamera.transform.position, hmdCamera.transform.forward);
        if (Physics.Raycast(ray, out var hit, maxDistance)) {
            return hit.collider.GetComponentInParent<Target>();
        }
        return null;
    }

    public override bool SelectionTriggered() {
        Target t = GetAimedTarget();
        if (t != current) {
            if (current) current.SetHighlighted(false);
            current = t;
            dwellCounter = 0f;
            if (current) current.SetHighlighted(true);
        }
        if (current == null) return false;

        dwellCounter += Time.deltaTime;
        if (dwellCounter >= dwellTime) {
            dwellCounter = 0f;
            return true;
        }
        return false;
    }
}

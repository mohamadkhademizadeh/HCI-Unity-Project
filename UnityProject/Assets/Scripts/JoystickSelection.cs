using UnityEngine;
using UnityEngine.InputSystem;

public class JoystickSelection : SelectionTechnique
{
    [SerializeField] private Transform reticle;
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private InputActionProperty moveAction;
    [SerializeField] private InputActionProperty selectAction;

    private bool enabledFlag;
    private Target current;

    void OnEnable() {
        moveAction.action?.Enable();
        selectAction.action?.Enable();
    }
    void OnDisable() {
        moveAction.action?.Disable();
        selectAction.action?.Disable();
    }

    public override void EnableTechnique(bool enable) {
        enabledFlag = enable;
        if (reticle) reticle.gameObject.SetActive(enable);
        if (current) current.SetHighlighted(false);
        current = null;
        gameObject.SetActive(enable);
    }

    public override Target GetAimedTarget() {
        if (!enabledFlag || reticle == null) return null;
        if (Physics.Raycast(reticle.position + Vector3.back * 0.1f, Vector3.forward, out var hit, 5f, targetMask)) {
            return hit.collider.GetComponentInParent<Target>();
        }
        return null;
    }

    public override bool SelectionTriggered() {
        if (!enabledFlag) return false;

        Vector2 move = moveAction.action?.ReadValue<Vector2>() ?? Vector2.zero;
        if (reticle != null && move.sqrMagnitude > 0f) {
            reticle.Translate(new Vector3(move.x, move.y, 0f) * moveSpeed * Time.deltaTime, Space.Self);
        }

        Target t = GetAimedTarget();
        if (t != current) {
            if (current) current.SetHighlighted(false);
            current = t;
            if (current) current.SetHighlighted(true);
        }

        bool pressed = (selectAction.action?.WasPressedThisFrame() ?? false);
        return pressed && current != null;
    }
}

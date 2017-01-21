using UnityEngine;

public class HealthUI : MonoBehaviour {
    public RectTransform healthBar;
    private const int _maxSize = 246;

    public void UpdateHealth(int currentHealth, int maxHealth) {
        healthBar.sizeDelta = new Vector2(_maxSize * currentHealth / maxHealth, healthBar.sizeDelta.y);
    }
}

using UnityEditor;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
    private Sprite startSprite;
    [SerializeField] private Sprite highlightedSprite;

    private SpriteRenderer spriteRenderer;

    private Overworld overworld;

    public bool Next { get; set; }

    public int CurrentLevelIndex { get; private set; }

    public Level Level { get; private set; }

    public void SetUp(int currentLevelIndex, Level level, Overworld overworld) {
        CurrentLevelIndex = currentLevelIndex;
        Level = level;
        this.overworld = overworld;
    }

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startSprite = spriteRenderer.sprite;
    }

    private void OnMouseEnter() {
        // Only allow interaction when this level object is next in line.
        if (Next) {
            if (overworld.CurrentState == GameState.WaitForMoveNext) {
                HighlightSprite(true);
            }
        }
    }

    private void OnMouseDown() {
        if (Next) {
            if (overworld.CurrentState == GameState.WaitForMoveNext) {
                overworld.GoNextLevel(CurrentLevelIndex, gameObject.transform.position);
            }
        }
    }

    private void OnMouseExit() {
        // Only allow interaction when this level object is next in line.
        if (Next) {
            if (overworld.CurrentState == GameState.WaitForMoveNext) {
                HighlightSprite(false);
            }
        }
    }

    /// <summary>
    ///     Enable or disable the highlighted sprite based on a passed bool.
    /// </summary>
    /// <param name="enter">Whether the highlight should be enabled or disabled.</param>
    public void HighlightSprite(bool enter) {
        if (enter) {
            spriteRenderer.sprite = highlightedSprite;
        } else {
            spriteRenderer.sprite = startSprite;
        }
    }
}
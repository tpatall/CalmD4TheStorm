using UnityEditor;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
    private Sprite startSprite;
    [SerializeField] private Sprite highlightedSprite;

    private SpriteRenderer spriteRenderer;

    private Overworld overworld;

    public bool Next;

    public int CurrentLevelIndex;

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

    private void Update() {
        if (overworld.PlayerPosition.x > transform.position.x) {
            Next = false;
            spriteRenderer.sprite = startSprite;
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
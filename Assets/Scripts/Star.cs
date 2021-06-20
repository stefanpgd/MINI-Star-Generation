using TMPro;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private GameObject star;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private StarInfo info;

    public void SetupStar(StarInfo info)
    {
        star.transform.localScale = new Vector3(info.GameSize, info.GameSize, info.GameSize);

        info = this.info;

        Color color = Random.ColorHSV();
        spriteRenderer.color = color;
    }
}

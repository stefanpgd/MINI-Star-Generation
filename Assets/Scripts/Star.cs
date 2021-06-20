using TMPro;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private GameObject star;
    [SerializeField] private ParticleSystem effect;
    [SerializeField] private SpriteRenderer starRenderer;
    [SerializeField] private SpriteRenderer innerStarRenderer;

    private StarInfo info;

    public void SetupStar(StarInfo info)
    {
        star.transform.localScale = new Vector3(info.GameSize, info.GameSize, info.GameSize);
        effect.gameObject.transform.localScale = new Vector3(info.GameSize, info.GameSize, info.GameSize);

        Color color = Random.ColorHSV();
        starRenderer.color = color;

        color.r += 0.1f;
        color.g += 0.1f;
        color.b += 0.1f;

        effect.startColor = color;
        innerStarRenderer.color = color;

        this.info = info;
    }
}

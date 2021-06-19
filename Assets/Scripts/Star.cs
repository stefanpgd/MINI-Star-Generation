using TMPro;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private GameObject star;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject moon1, moon2;

    private StarInfo info;

    public void SetupStar(StarInfo info)
    {
        star.transform.localScale = new Vector3(info.GameSize, info.GameSize, info.GameSize);

        info = this.info;

        moon1.SetActive(false);
        moon2.SetActive(false);

        int roll = Random.Range(0, 100);
        if(roll > 50)
        {
            moon1.SetActive(true);

            int roll2 = Random.Range(0, 100);

            if(roll2 > 50)
            {
                moon2.SetActive(true);
            }
        }

        Color color = Random.ColorHSV();
        spriteRenderer.color = color;
    }

    private float angle = 0f;
    private float rotateSpeed = 90f;

    private void Update()
    {


        angle += rotateSpeed * Time.deltaTime;

        star.transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}

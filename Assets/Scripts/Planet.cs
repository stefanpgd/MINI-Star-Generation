using UnityEngine;

public class Planet : MonoBehaviour
{
    public PlanetInfo Info;

    [SerializeField] private GameObject planet;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject moon;
    [SerializeField] private SpriteRenderer moonRenderer;

    private float angleFromSun;
    private float localAngle;
    private float orbitalRotationVelocity;
    private float localRotationVelocity;

    private float moonAngle;
    private float moonDistance;
    private float orbitalRotationVelocityMoon;

    public void SetupPlanet(PlanetInfo info)
    {
        planet.transform.localScale = new Vector3(info.GameSize, info.GameSize, info.GameSize);

        Color color = Random.ColorHSV();
        spriteRenderer.color = color;

        angleFromSun = Random.Range(-180, 180);
        localAngle = Random.Range(-180, 180);
        localRotationVelocity = Random.Range(90f, 180f);
        orbitalRotationVelocity = Random.Range(0.01f, 1f);

        Info = info;

        int roll = Random.Range(0, 100);

        if(roll > 50)
        {
            moon.gameObject.SetActive(true);
            Color moonColor = Random.ColorHSV();
            moonRenderer.color = moonColor;

            moonAngle = Random.Range(0, 360);
            moonDistance = Random.Range(info.GameSize + 0.25f, info.GameSize + 0.75f);
            orbitalRotationVelocityMoon = Random.Range(1, 3f);
        }
        else
        {
            moon.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        localAngle += localRotationVelocity * Time.deltaTime;
        angleFromSun += orbitalRotationVelocity * Time.deltaTime;

        float x = Info.DistanceFromStar * Mathf.Cos(angleFromSun);
        float y = Info.DistanceFromStar * Mathf.Sin(angleFromSun);

        planet.transform.position = new Vector3(x, 0, y);
        //planet.transform.localRotation = Quaternion.Euler(0f, 0f, localAngle);

        if(moon.activeSelf)
        {
            moonAngle += orbitalRotationVelocityMoon * Time.deltaTime;

            float mx = moonDistance * Mathf.Cos(moonAngle);
            float my = moonDistance * Mathf.Sin(moonAngle);

            mx += x;
            my += y;

            moon.transform.position = new Vector3(mx, 0, my);
        }
    }
}
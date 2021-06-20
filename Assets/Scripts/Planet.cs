using UnityEngine;

public class Planet : MonoBehaviour
{
    public PlanetInfo Info;
    [HideInInspector] public bool HorizontalMovement = true;

    [SerializeField] private GameObject planet;
    [SerializeField] private SpriteRenderer planetRenderer;
    [SerializeField] private SpriteRenderer innenPlanetRenderer;
    [SerializeField] private GameObject moon;
    [SerializeField] private SpriteRenderer moonRenderer;

    private float angleFromSun;
    private float localAngle;
    private float orbitalRotationVelocity;
    private float localRotationVelocity;

    private float moonAngle;
    private float moonDistance;
    private float orbitalRotationVelocityMoon;

    private float orbitOffsetMultiplier;

    public void SetupPlanet(PlanetInfo info)
    {
        planet.transform.localScale = new Vector3(info.GameSize, info.GameSize, info.GameSize);

        Color planetColor = Random.ColorHSV();
        planetRenderer.color = planetColor;

        planetColor.r += 0.1f;
        planetColor.g += 0.1f;
        planetColor.b += 0.1f;
        innenPlanetRenderer.color = planetColor;

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

        orbitOffsetMultiplier = Random.Range(2.5f, 10f);
    }

    private void Update()
    {
        localAngle += localRotationVelocity * Time.deltaTime;
        angleFromSun += orbitalRotationVelocity * Time.deltaTime;

        float x = Info.DistanceFromStar * Mathf.Cos(angleFromSun);
        float y = Info.DistanceFromStar * Mathf.Sin(angleFromSun);

        if(HorizontalMovement)
        {
            planet.transform.position = new Vector3(x, y, 0f);
        }
        else
        {
            planet.transform.position = new Vector3(x, (x + y) / orbitOffsetMultiplier, y);
        }

        planet.transform.localRotation = Quaternion.Euler(0f, 0f, localAngle);

        if(moon.activeSelf)
        {
            moonAngle += orbitalRotationVelocityMoon * Time.deltaTime;

            float mx = moonDistance * Mathf.Cos(moonAngle);
            float my = moonDistance * Mathf.Sin(moonAngle);

            mx += x;
            my += y;


            if(HorizontalMovement)
            {
                moon.transform.position = new Vector3(mx, my, 0);
            }
            else
            {
                moon.transform.position = new Vector3(mx, (mx + my) / orbitOffsetMultiplier, my);
            }
        }
    }
}

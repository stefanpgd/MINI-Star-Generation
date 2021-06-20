using TMPro;
using UnityEngine;

public class StarSystemManager : MonoBehaviour
{
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private GameObject planetPrefab;
    [SerializeField] private TMP_Text nameText, massText, diameterText, ageText;

    #region Singleton
    public static StarSystemManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    private float sunSize;
    private float sunAge;

    private float lastStarDistance;

    public void CreateSystem(string seedName)
    {
        int seed = seedName.GetHashCode();
        Debug.Log("seed:" + seed);
        Random.InitState(seed);

        SetupStar();
        SetupPlanet(true);
        SetupPlanet(false);
        SetupPlanet(false);
    }

    private void SetupStar()
    {
        StarInfo info = new StarInfo();

        char[] name = new char[10];

        name[0] = (char)Random.Range(65, 90);
        name[1] = (char)Random.Range(48, 57);
        name[2] = '-';

        for(int i = 3; i < name.Length; i++)
        {
            name[i] = (char)Random.Range(97, 122);
        }

        info.Name = new string(name);
        info.GameSize = Random.Range(1f, 3f);
        sunSize = info.GameSize;

        double mass = Random.Range(0.3f, 12.5f);
        mass = System.Math.Round(mass, 2);
        info.Mass = mass;

        info.Diameter = Random.Range(75000, 1950000);

        double age = Random.Range(1f, 10f);
        age = System.Math.Round(age, 2);
        info.Age = age;
        sunAge = (float)age;

        GameObject s = Instantiate(starPrefab, Vector3.zero, Quaternion.identity);
        Star star = s.GetComponent<Star>();

        star.SetupStar(info);

        nameText.text = info.Name;
        massText.text = info.Mass.ToString() + " - Solar Masses";
        diameterText.text = info.Diameter + " - Kilometers";
        ageText.text = info.Age + " - Billion Years old";
    }

    private void SetupPlanet(bool first)
    {
        PlanetInfo info = new PlanetInfo();

        char[] name = new char[10];

        name[0] = (char)Random.Range(65, 90);
        name[1] = (char)Random.Range(48, 57);
        name[2] = '-';

        for(int i = 3; i < name.Length; i++)
        {
            name[i] = (char)Random.Range(97, 122);
        }

        info.Name = new string(name);
        info.GameSize = Random.Range(0.25f, sunSize - 1f);

        if(first)
        {
            info.DistanceFromStar = Random.Range(3f, 7.5f);
            lastStarDistance = info.DistanceFromStar;
        }
        else
        {
            info.DistanceFromStar = Random.Range(lastStarDistance + 1.5f, lastStarDistance + 3.5f);
            lastStarDistance = info.DistanceFromStar;
        }

        double mass = Random.Range(0.01f, 0.5f);
        mass = System.Math.Round(mass, 2);
        info.Mass = mass;

        info.Diameter = Random.Range(7500, 100000);

        double age = Random.Range(0.1f, sunAge);
        age = System.Math.Round(age, 2);
        info.Age = age;

        GameObject p = Instantiate(planetPrefab, Vector3.zero, Quaternion.identity);
        Planet planet = p.GetComponent<Planet>();

        planet.SetupPlanet(info);
    }
}

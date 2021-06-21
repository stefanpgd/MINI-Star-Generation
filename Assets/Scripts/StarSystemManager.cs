using SilverRogue.Tools;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarSystemManager : MonoBehaviour
{
    public Action orientationUpdatedEvent = delegate { };

    [SerializeField] private GameObject starPrefab;
    [SerializeField] private GameObject planetPrefab;
    [SerializeField] private TMP_Text nameText, massText, diameterText, ageText;
    [SerializeField] private Animator switchAnimator;
    [SerializeField] private float switchDelay;

    private StarfieldBackground starfieldBackground;
    private List<Planet> planets = new List<Planet>();
    private Timer switchTimer;

    private float sunSize;
    private float sunAge;
    private float lastStarDistance;

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

    private void Start()
    {
        starfieldBackground = StarfieldBackground.Instance;

        switchTimer = new Timer(switchDelay);
    }

    private void Update()
    {
        if(switchTimer.Expired)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                switchAnimator.SetTrigger("Switch");
                switchTimer = new Timer(switchDelay);
                switchTimer.timerExpiredEvent += SwitchPlanetOrientation;
            }
        }
    }

    public void CreateSystem(string seedName)
    {
        int seed = seedName.GetHashCode();
        Debug.Log("seed:" + seed);
        UnityEngine.Random.InitState(seed);

        SetupStar();
        SetupPlanet(true);
        SetupPlanet(false);
        SetupPlanet(false);

        starfieldBackground.CreateStarfield();
    }

    private void SetupStar()
    {
        StarInfo info = new StarInfo();

        char[] name = new char[10];

        name[0] = (char)UnityEngine.Random.Range(65, 90);
        name[1] = (char)UnityEngine.Random.Range(48, 57);
        name[2] = '-';

        for(int i = 3; i < name.Length; i++)
        {
            name[i] = (char)UnityEngine.Random.Range(97, 122);
        }

        info.Name = new string(name);
        info.GameSize = UnityEngine.Random.Range(1f, 3f);
        sunSize = info.GameSize;

        double mass = UnityEngine.Random.Range(0.3f, 12.5f);
        mass = System.Math.Round(mass, 2);
        info.Mass = mass;

        info.Diameter = UnityEngine.Random.Range(75000, 1950000);

        double age = UnityEngine.Random.Range(1f, 10f);
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

        name[0] = (char)UnityEngine.Random.Range(65, 90);
        name[1] = (char)UnityEngine.Random.Range(48, 57);
        name[2] = '-';

        for(int i = 3; i < name.Length; i++)
        {
            name[i] = (char)UnityEngine.Random.Range(97, 122);
        }

        info.Name = new string(name);
        info.GameSize = UnityEngine.Random.Range(0.75f, sunSize - 0.5f);

        if(first)
        {
            info.DistanceFromStar = UnityEngine.Random.Range(5f, 10f);
            lastStarDistance = info.DistanceFromStar;
        }
        else
        {
            info.DistanceFromStar = UnityEngine.Random.Range(lastStarDistance + 2f, lastStarDistance + 5.5f);
            lastStarDistance = info.DistanceFromStar;
        }

        double mass = UnityEngine.Random.Range(0.01f, 0.5f);
        mass = System.Math.Round(mass, 2);
        info.Mass = mass;

        info.Diameter = UnityEngine.Random.Range(7500, 100000);

        double age = UnityEngine.Random.Range(0.1f, sunAge);
        age = System.Math.Round(age, 2);
        info.Age = age;

        GameObject p = Instantiate(planetPrefab, Vector3.zero, Quaternion.identity);
        Planet planet = p.GetComponent<Planet>();

        planet.SetupPlanet(info);
        planets.Add(planet);
    }

    private void SwitchPlanetOrientation()
    {
        planets.ForEach(planet => planet.HorizontalMovement = !planet.HorizontalMovement);
        orientationUpdatedEvent.Invoke();
    }
}

using System.Collections.Generic;
using UnityEngine;

public class StarfieldBackground : MonoBehaviour
{
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private int spawnAmount;
    [SerializeField] private Vector2 boundaries;
    [SerializeField] private float zDistance;

    [Header("Star Settings")]
    [SerializeField] private Color starColor;
    [SerializeField] private bool randomRotation;
    [SerializeField] private bool dimStarsRandomly;

    private List<GameObject> stars = new List<GameObject>();

    #region Singleton
    public static StarfieldBackground Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    public void CreateStarfield()
    {
        if(stars.Count > 0)
        {
            foreach(GameObject star in stars)
            {
                star.SetActive(false);
            }
        }

        for(int i = 0; i < spawnAmount; i++)
        {
            Vector3 position = new Vector3();
            position.x = Random.Range(-boundaries.x, boundaries.x);
            position.y = Random.Range(-boundaries.y, boundaries.y);
            position.z = zDistance;

            GameObject star = Instantiate(starPrefab, position, Quaternion.identity);

            if(randomRotation)
            {
                float angle = Random.Range(-180, 180);
                star.transform.localRotation = Quaternion.Euler(0, 0, angle);
            }

            SpriteRenderer renderer = star.GetComponent<SpriteRenderer>();

            if(dimStarsRandomly)
            {
                float dim = Random.Range(0, 1f);
                starColor.a = dim;
            }

            renderer.color = starColor;

            stars.Add(star);
        }
    }
}

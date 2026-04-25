using UnityEngine;

public class CellRenderer : MonoBehaviour
{
    // Circle

    [Header("Circle Size")]
    [SerializeField] private int numberOfPoints = 15;
    private float minRadius = 0.05f;

    // Perlin noise variable

    [Header("Perlin Noise")]
    [SerializeField] private float xIncrement = 0.2f;
    [SerializeField] private float yIncrement = 0.15f;
    [SerializeField] private float timeIncrement = 0.1f;

    private float xoff;
    private float yoff;

    // Points
    private Vector2[] points;
    
    [SerializeField] private LineRenderer lineRenderer;
    private CellCore cellCore;

    private void Awake()
    {
        cellCore = gameObject.GetComponent<CellCore>();
    }

    private void Start()
    {  
        lineRenderer.positionCount = numberOfPoints;

        points = new Vector2[numberOfPoints];
        xoff = Random.Range(0, 10000);
        yoff = Random.Range(0, 10000);

        points = CalculatePointsCoordonate();
    }

    private void Update()
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
            lineRenderer.SetPosition(i, points[i]);
        }

        xoff += timeIncrement * Time.deltaTime;
        yoff += timeIncrement * Time.deltaTime;

        points = CalculatePointsCoordonate();
    }

    private Vector2[] CalculatePointsCoordonate()
    {
        Vector2[] tempPoints = new Vector2[numberOfPoints];

        float startingXoff = xoff;
        float startingYoff = yoff;

        for (int i = 0; i < numberOfPoints; i++)
        {
            float pointAngle = ((float)i / (float)numberOfPoints) * (2 * Mathf.PI);
            float distance = Mathf.PerlinNoise(xoff, yoff);
            distance = Utils.Map(distance, 0f, 1f, minRadius, cellCore.GetRadius());

            float x = transform.position.x + (distance * Mathf.Cos(pointAngle));
            float y = transform.position.y + (distance * Mathf.Sin(pointAngle));

            tempPoints[i] = new Vector2(x, y);

            xoff += xIncrement;
            yoff += yIncrement;
        }

        xoff = startingXoff;
        yoff = startingYoff;

        return tempPoints;
    }
}

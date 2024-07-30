using System.Collections.Generic;
using UnityEngine;

public class CardPositionCalculator// : MonoBehaviour
{
    public Vector2 centerPosition = Vector2.zero;
    
    private float baseRadius = 3.08f;

    private float xAxisAdjustment;
    private float yAxisAdjustment;
    private float angleOffset;

    private float elseOffset = 1.571f;
    private float eightOffset = 1.597f;

    private float xFirstHalf = 1.05f;
    private float yFirstHalf = 1f;
    private float xSecondHalf = 1.2f;
    private float ySecondHalf = 1.2f;
    private float angleCorrection = 0.785f;


    private List<Vector3> cardPoints = new List<Vector3>();
    private List<Transform> cards;

    public CardPositionCalculator()
    {
        GetCards();

        angleOffset = cards.Count == 8 ? eightOffset : elseOffset;

        AdjustCardPositioningParameters(); 

        CalculatePositions();
    }
    public void Initialize()
    {
        GetCards();

        angleOffset = cards.Count == 8 ? eightOffset : elseOffset;

        AdjustCardPositioningParameters(); 

        CalculatePositions();
    }

    private void AdjustCardPositioningParameters()
    {
        switch (cards.Count)
        {
            case < 5:
                xAxisAdjustment = xFirstHalf;
                yAxisAdjustment = yFirstHalf;
                break;
            case 5:
                xAxisAdjustment = 1.3f;
                yAxisAdjustment = 1.3f;
                angleOffset = angleCorrection;
                break;
            default:
                xAxisAdjustment = xSecondHalf;
                yAxisAdjustment = ySecondHalf;
                break;
        }
    }

    private void GetCards()
    {
        cards = CardFactory.Cards;
    }

    private void CalculatePositions()
    {
        float angleStep;

        if (cards.Count > 4)
        {
            angleStep = 360 / (cards.Count - 1) * Mathf.Deg2Rad;
        }
        else
        {
            angleStep = 360 / cards.Count * Mathf.Deg2Rad;

        }
        float screenAspectRatio = (float)Screen.width / Screen.height;

        float xRadius = baseRadius * screenAspectRatio * xAxisAdjustment;
        float yRadius = baseRadius * yAxisAdjustment;

        for (int i = 0; i < cards.Count; i++)
        {
            float angle = angleStep * i + angleOffset;
            Vector2 localPosition = new Vector2(Mathf.Cos(angle) * xRadius, Mathf.Sin(angle) * yRadius);

            cardPoints.Add(new Vector3(localPosition.x + centerPosition.x, localPosition.y + centerPosition.y,
                cards[i].position.z));
        }
    }

    public List<Vector3> GetPoints() { return cardPoints; }
    
}

using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class WeaponTrajectory : MonoBehaviour
{
    [SerializeField] private Transform shootingPoint;

    [SerializeField] private GameObject lineDotPrefab;

    private List<GameObject> trajectoryPoints = new List<GameObject>();

    [SerializeField] private int pointsCount;

    [SerializeField] private float dotSpacing;

    [SerializeField] private PlayerShooting playerShooting;


    private void OnEnable()
    {
        ShowPoints(true);
    }

    private void OnDisable()
    {
        ShowPoints(false);
    }

    public void ShowPoints(bool show)
    {
        if (trajectoryPoints.Count <= 0) return;
        foreach(var point in trajectoryPoints)
        {
            if (point == null) continue;
            point.gameObject.SetActive(show);
        }
    }

    private void Start()
    {
        GenerateDots();
    }

    private void GenerateDots()
    {
        for (int i = 0; i < pointsCount; i++)
        {
            GameObject gob = Instantiate(lineDotPrefab, shootingPoint.transform.position, shootingPoint.transform.rotation);
            trajectoryPoints.Add(gob);
        }
    }

    private void Update()
    {
        UpdateTrajPointsPositions();
        
    }

    private void FixedUpdate()
    {
        OnClickEnableAndDisableTrajLines();
    }


    private void OnClickEnableAndDisableTrajLines ()
    {
        if(Input.GetKey(KeyCode.Mouse1))
        {
            ShowPoints(!trajectoryPoints[0].gameObject.activeInHierarchy);
        }
    }
    private void UpdateTrajPointsPositions()
    {
        for (int i = 0; i < trajectoryPoints.Count; i++)
        {
            trajectoryPoints[i].transform.position = TrajPointPos(i * dotSpacing);
        }
    }

    public Vector2 TrajPointPos(float t)
    {
        Vector2 pos = (Vector2)shootingPoint.transform.position + (playerShooting.TrajDir.normalized * playerShooting.BulletForce * t) +
                      0.5f * Physics2D.gravity * (t * t);

        return pos;
    }
}

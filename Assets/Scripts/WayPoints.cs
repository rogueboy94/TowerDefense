using UnityEngine;

public class WayPoints : MonoBehaviour {
    public static Transform[] way_points;

    void Awake()
    {
        way_points = new Transform[transform.childCount];
        for (int i = 0; i < way_points.Length; i++)
        {
            way_points[i] = transform.GetChild(i);
        } 
    }


}

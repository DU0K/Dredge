using UnityEngine;

public class FishManager : InputManager
{
    [System.Serializable]
    public class Fish
    {
        public float speed;
        public QTELocation[] Locations;
    }

    [System.Serializable]
    public class QTELocation
    {
        public float location;
        public float size;
    }

    [SerializeField] private Fish[] fish;
    public Fish[] _fish { get { return fish; } }
}

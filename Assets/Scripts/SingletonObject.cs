using UnityEngine;

public class SingletonObject<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = (T)FindObjectOfType(typeof(T));
            }

            return _instance;
        }
    }
}
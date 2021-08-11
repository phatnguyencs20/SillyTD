using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int credit;

    [SerializeField]
    private int initialCredit;

    private void Start()
    {
        credit = initialCredit;
    }
}

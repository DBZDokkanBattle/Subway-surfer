using UnityEngine;
using TMPro;

public class MaxSpeedTracker : MonoBehaviour
{
    public enum SpeedUnit { MetersPerSecond, KilometersPerHour, MilesPerHour }

    [Header("What to track")]
    [SerializeField] private Transform target;              
    [SerializeField] private bool warnIfNoTarget = true;

    [Header("UI (optional)")]
    [SerializeField] private TextMeshProUGUI currentSpeedText; 
    [SerializeField] private TextMeshProUGUI maxSpeedText;     
    [SerializeField] private string currentPrefix = "Current Speed: ";
    [SerializeField] private string maxPrefix = "Max Speed: ";

    [Header("Display")]
    [SerializeField] private SpeedUnit unit = SpeedUnit.MetersPerSecond;
    [SerializeField] private float worldScaleToMeters = 1f; 
    [Range(0f, 0.9f)][SerializeField] private float smoothing = 0.2f;

    public float CurrentSpeed { get; private set; } 
    public float MaxSpeed { get; private set; }     

    Rigidbody rb; Rigidbody2D rb2d;
    Vector3 prevPos; bool hasPrev;

    void Start() { CachePhysicsComponents(); }

    void Update()
    {
        if (target == null)
        {
            if (warnIfNoTarget) { Debug.LogWarning($"{nameof(MaxSpeedTracker)}: No target set."); warnIfNoTarget = false; }
            return;
        }

        // compute speed in m/s
        float mps = 0f;
        if (rb != null) mps = rb.linearVelocity.magnitude * worldScaleToMeters;
        else if (rb2d != null) mps = rb2d.linearVelocity.magnitude * worldScaleToMeters;
        else
        {
            if (!hasPrev) { prevPos = target.position; hasPrev = true; return; }
            var delta = (target.position - prevPos) * worldScaleToMeters;
            mps = delta.magnitude / Mathf.Max(Time.deltaTime, 1e-6f);
            prevPos = target.position;
        }

       
        float disp = FromMps(mps);

        if (smoothing > 0f)
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, disp, 1f - Mathf.Pow(1f - smoothing, Time.deltaTime * 60f));
        else
            CurrentSpeed = disp;

        if (CurrentSpeed > MaxSpeed) MaxSpeed = CurrentSpeed;

       
        if (currentSpeedText) currentSpeedText.text = currentPrefix + Format(disp);
        if (maxSpeedText) maxSpeedText.text = maxPrefix + Format(MaxSpeed);
    }

    public void ResetMax()
    {
        MaxSpeed = 0f;
        if (maxSpeedText) maxSpeedText.text = maxPrefix + Format(0f);
    }

    public void ResetAll()
    {
        hasPrev = false; CurrentSpeed = 0f; MaxSpeed = 0f;
        if (currentSpeedText) currentSpeedText.text = currentPrefix + Format(0f);
        if (maxSpeedText) maxSpeedText.text = maxPrefix + Format(0f);
    }

    public void SetTarget(Transform t)
    {
        target = t; hasPrev = false; CachePhysicsComponents();
    }

    void CachePhysicsComponents()
    {
        rb = null; rb2d = null;
        if (target == null) return;
        rb = target.GetComponent<Rigidbody>();
        rb2d = target.GetComponent<Rigidbody2D>();
    }

    float FromMps(float mps) => unit switch
    {
        SpeedUnit.KilometersPerHour => mps * 3.6f,
        SpeedUnit.MilesPerHour => mps * 2.2369363f,
        _ => mps
    };

    string UnitSuffix() => unit switch
    {
        SpeedUnit.KilometersPerHour => "km/h",
        SpeedUnit.MilesPerHour => "mph",
        _ => "m/s"
    };

    string Format(float v) => $"{v:0.00} {UnitSuffix()}";
}

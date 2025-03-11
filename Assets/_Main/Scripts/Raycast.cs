using UnityEngine;

public class Raycast : MonoBehaviour
{
    public LineRenderer line;
    [SerializeField] Transform endPos, display;
    Vector3 endLine;
    public LayerMask layerMask;
    public Collider raycastingObj = null;
    RaycastHit hit;

    [SerializeField] bool _isActive = true;
    public bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;
            if (!_isActive)
            {
                line.enabled = false;
                display.gameObject.SetActive(false);
            }
            else
            {
                line.enabled = true;
                display.gameObject.SetActive(true);
            }
        }
    }

    public int curveResolution = 20;
    private void Start()
    {
        IsActive = true;
    }
    public virtual void Update()
    {
        if (!_isActive)
        {
            return;
        }

        if (IsRaycast(out Vector3 point))
        {
            endLine = point;
            display.position = endLine;
        }
        else
        {
            raycastingObj = null;
            display.position = endPos.position;
        }

        endLine = endPos.position;

        if (raycastingObj != null && raycastingObj.CompareTag("teleport"))
        {
            CreateFishingLineCurve(transform.position, display.position);
        }
        else
        {
            if (line.positionCount != 2)
                line.positionCount = 2;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, display.position);
        }

        AdjustDisplayScale(transform.position, display.position);

    }
    bool IsRaycast(out Vector3 point)
    {
        point = Vector3.zero;
        if (Physics.Raycast(transform.position, (endPos.position - transform.position).normalized, out hit, 100, layerMask))
        {
            if (hit.collider != null)
            {
                point = hit.point;
                raycastingObj = hit.collider;
                return true;
            }
        }
        return false;
    }
    public Vector3 Direction()
    {
        return endPos.position - transform.position;
    }
    public void SetEndPos(Vector3 pos)
    {
        endPos.position = pos;
    }
    [SerializeField] float thresholdHigh = 6;
    // Hàm tạo đường cong
    void CreateFishingLineCurve(Vector3 start, Vector3 end)
    {
        if (line.positionCount != curveResolution)
            line.positionCount = curveResolution;

        float distance = Vector3.Distance(start, end);
        float thresholdHighReal = distance / thresholdHigh;
        float maxHeight = Mathf.Clamp(distance / thresholdHigh, 0.1f, 1.0f); // Điều chỉnh chiều cao tối đa của đường cong

        for (int i = 0; i < curveResolution; i++)
        {
            float t = i / (float)(curveResolution - 1);
            float height = Mathf.Sin(t * Mathf.PI) * maxHeight; // Điều chỉnh chiều cao của đường cong dựa trên khoảng cách
            Vector3 point = Vector3.Lerp(start, end, t) + Vector3.up * height;
            line.SetPosition(i, point);
        }


    }
    [SerializeField] float minScale = 0.5f; // Tỷ lệ nhỏ nhất của display
    [SerializeField] float maxScale = 2.0f;
    // Hàm điều chỉnh tỷ lệ của display
    void AdjustDisplayScale(Vector3 start, Vector3 end)
    {
        float distance = Vector3.Distance(start, end);
        float scale = Mathf.Lerp(minScale, maxScale, distance / 100.0f); // Điều chỉnh tỷ lệ dựa trên khoảng cách
        display.localScale = new Vector3(scale, scale, scale);
    }
}

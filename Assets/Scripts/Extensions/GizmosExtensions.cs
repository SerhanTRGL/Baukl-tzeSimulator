using UnityEngine;

public static class GizmosExtensions {
    public static void DrawCapsule(Vector3 position, Vector3 direction, float height, float radius) {
        Vector3 top = position + direction * (height / 2 - radius);  // Top sphere center
        Vector3 bottom = position;  // Bottom sphere center

        // Draw the top and bottom spheres
        Gizmos.DrawWireSphere(top, radius);
        Gizmos.DrawWireSphere(bottom, radius);

        // Draw the connecting cylinder with lines
        Vector3 right = Vector3.Cross(direction, Vector3.up).normalized * radius;
        Vector3 forward = Vector3.Cross(direction, right).normalized * radius;

        Gizmos.DrawLine(top + right, bottom + right);
        Gizmos.DrawLine(top - right, bottom - right);
        Gizmos.DrawLine(top + forward, bottom + forward);
        Gizmos.DrawLine(top - forward, bottom - forward);
    }
}
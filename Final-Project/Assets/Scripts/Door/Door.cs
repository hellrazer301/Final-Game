using System.Collections;
using UnityEngine;

namespace DoorScript
{
    [RequireComponent(typeof(AudioSource))]
    public class Door : MonoBehaviour
    {
        public bool open;
        public float smooth = 1.0f;

        float DoorOpenAngle = -90.0f;
        float DoorCloseAngle = 0.0f;

        public AudioSource asource;
        public AudioClip openDoor, closeDoor;

        // Door behavior
        public enum DoorType { Normal, Locked }
        public DoorType doorType = DoorType.Normal;

        // Auto close
        public bool autoClose = true;
        public float closeDelay = 1.5f;

        // Shake settings
        public float shakeDuration = 0.4f;
        public float shakeAmount = 2f;

        private Coroutine closeRoutine;
        private Quaternion originalRotation;

        void Start()
        {
            asource = GetComponent<AudioSource>();
            originalRotation = transform.localRotation;
        }

        void Update()
        {
            if (open)
            {
                var target = Quaternion.Euler(0, DoorOpenAngle, 0);
                transform.localRotation =
                    Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * 5 * smooth);
            }
            else
            {
                var target1 = Quaternion.Euler(0, DoorCloseAngle, 0);
                transform.localRotation =
                    Quaternion.Slerp(transform.localRotation, target1, Time.deltaTime * 5 * smooth);
            }
        }

        //CALLED BY PLAYER / TRIGGER
        public void OpenDoor()
        {
            if (doorType == DoorType.Locked)
            {
                StartCoroutine(ShakeDoor());
                return;
            }

            open = !open;
            asource.clip = open ? openDoor : closeDoor;
            asource.Play();

            if (open && autoClose)
            {
                if (closeRoutine != null)
                    StopCoroutine(closeRoutine);

                closeRoutine = StartCoroutine(AutoClose());
            }
        }

        IEnumerator AutoClose()
        {
            yield return new WaitForSeconds(closeDelay);
            open = false;
            asource.clip = closeDoor;
            asource.Play();
        }

        IEnumerator ShakeDoor()
        {
            float elapsed = 0f;

            while (elapsed < shakeDuration)
            {
                elapsed += Time.deltaTime;
                float y = Mathf.Sin(elapsed * 50f) * shakeAmount;
                transform.localRotation =
                    originalRotation * Quaternion.Euler(0, y, 0);
                yield return null;
            }

            transform.localRotation = originalRotation;
        }

        public void OpenAwayFrom(Transform player)
        {
            Vector3 doorToPlayer = player.position - transform.position;

            // Is player in front or behind the door?
            float dot = Vector3.Dot(transform.forward, doorToPlayer);

            // If dot > 0 → player is in front, open backward
            // If dot < 0 → player is behind, open forward
            float direction = dot < 0 ? -1f : 1f;

            DoorOpenAngle = -90f * direction;

            OpenDoor();
        }

    }
}

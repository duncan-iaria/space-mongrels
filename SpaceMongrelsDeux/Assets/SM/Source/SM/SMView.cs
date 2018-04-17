using UnityEngine;
using SNDL;

namespace SM
{
    public class SMView : View
    {
        public Rigidbody2D currentRb;

        //distance camera is from the player (in the Z axis )
        public float cameraDistance;

        public float moveDampening;

        //how much the camera leads the players direction of motion (so the camera looks ahead)
        public float leadDistanceModifier;

        [Header("Zoom Properties")]
        public float zoomDampening;         //how quickly the zooming in/out interpolates between values
        public float zoomMultiplier;        //boosts the changeIn values receieved from player
        public float minZoom;
        public float maxZoom;

        [Header("Shake Properties")]
        public bool isShaking;
        public float shakeDuration;
        public float shakeMagnitude;
        public float shakeTimer;
        private Vector3 shakeVector;

        //for calculating the camera drag
        protected float currentX;
        protected float wantedX;
        protected float currentY;
        protected float wantedY;

        //for zooming camera in and out
        protected float totalChangeInPos;
        protected float currentOrthoSize;
        protected float wantedOrthoSize;

        protected SMPawn currentPawn;

        //for resetting the shake info (incase different values are passed in)
        protected float initShakeMagnitude;
        protected float initShakeDuration;

        public void Start()
        {
            if (cam != null)
            {
                currentOrthoSize = cam.orthographicSize;
            }

            if (currentTarget != null)
            {
                setTarget(currentTarget);
            }

            //get the initial shake values
            initShakeMagnitude = shakeMagnitude;
            initShakeDuration = shakeDuration;
        }

        void FixedUpdate()
        {
            //if there is no target assigned the update breaks out
            if (!currentTarget)
            {
                return;
            }

            currentX = transform.position.x;
            wantedX = currentTarget.position.x;
            currentY = transform.position.y;
            wantedY = currentTarget.position.y;

            //if we're following a player pawn - apply the target location to predict which direction
            //the player is heading. This gives the camera some lead space
            //Additionally the zoom is calculated by getting the average change in speed (axis compounded)
            if (currentRb != null)
            {
                wantedX += currentRb.velocity.x * leadDistanceModifier;
                wantedY += currentRb.velocity.y * leadDistanceModifier;

                wantedOrthoSize = currentRb.velocity.sqrMagnitude * zoomMultiplier;
                wantedOrthoSize = Mathf.Clamp(wantedOrthoSize, minZoom, maxZoom);

                //old ortho
                //wantedOrthoSize = Mathf.Abs( currentRb.velocity.y * zoomMultiplier ) + Mathf.Abs( currentRb.velocity.x * zoomMultiplier );

                //this could all be in the above line, but I seperated for readability
                //wantedOrthoSize = Mathf.Clamp( wantedOrthoSize, minZoom, maxZoom );
            }

            currentX = Mathf.Lerp(currentX, wantedX, moveDampening * Time.deltaTime);
            currentY = Mathf.Lerp(currentY, wantedY, moveDampening * Time.deltaTime);
            currentOrthoSize = Mathf.Lerp(currentOrthoSize, wantedOrthoSize, zoomDampening * Time.deltaTime);
            Debug.Log("Current Ortho size: " + currentOrthoSize);
            shakeTimer += Time.deltaTime;

            //If shake is over
            if (shakeTimer > shakeDuration)
            {
                isShaking = false;
                shakeDuration = initShakeDuration;
                shakeMagnitude = initShakeMagnitude;
            }

            if (isShaking)
            {
                shakeVector = shake();
            }
            else
            {
                shakeVector = Vector3.zero;
            }

            transform.position = new Vector3(currentX, currentY, -cameraDistance) + shakeVector;
            //Debug.Log( currentOrthoSize );
            cam.orthographicSize = currentOrthoSize;
        }

        public override void setTarget(Transform tTarget, bool isImmediate = false)
        {
            base.setTarget(tTarget, isImmediate);
            currentRb = tTarget.GetComponent<Rigidbody2D>();
        }

        //this is the code that actually shakes the camera
        //while isShaking is true, this executes
        private Vector3 shake()
        {
            float percentComplete = shakeTimer / shakeDuration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= shakeMagnitude * damper;
            y *= shakeMagnitude * damper;

            return new Vector3(x, y, 0f);
        }

        ///<summary>Used for triggering camera shake from other components</summary>
        /// <param name="tShakeMagnitude">How much the camera will shake.</param>
        /// <param name="tShakeDuration">How long the camera will shake for</param>
        public virtual void shakeInit(float tShakeMagnitude, float tShakeDuration)
        {
            //assign defaults
            if (tShakeMagnitude == 0f)
            {
                tShakeDuration = shakeDuration;
                tShakeMagnitude = shakeMagnitude;
            }
            shakeDuration = tShakeDuration;
            shakeMagnitude = tShakeMagnitude;
            shakeTimer = 0f;
            isShaking = true;

            //Debug.Log( "Cam Shake Engaged!" );
        }
    }
}

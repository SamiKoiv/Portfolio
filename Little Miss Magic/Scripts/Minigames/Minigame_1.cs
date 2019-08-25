using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame_1
{
    public class Minigame_1 : MonoBehaviour
    {
        public float GameDuration;
        public float ShakeModifier;
        public float CenterShakeModifier;
        public float ShakeDuration = 1;
        public AnimationCurve ShakeCurve;

        public RectTransform Center;
        public RectTransform Up;
        public RectTransform Left;
        public RectTransform Right;
        public RectTransform Down;

        public ParticleSystem AnswerParticles;
        public ParticleSystem ScoreParticles;

        public Light JuiceLight;
        public float JuiceLightModifier;

        Image centerImage;
        Image upImage;
        Image leftImage;
        Image rightImage;
        Image downImage;

        public TextMeshProUGUI timerText;
        public TextMeshProUGUI scoreText;

        public Transform OrbsParent;
        public GameObject Orb;

        bool up_down;
        bool left_down;
        bool right_down;
        bool down_down;

        Transform[] orbs;

        new Transform transform;

        Request currentRequest;

        float timer;
        int score;

        float deltaTime;

        float pulse = 0;
        public float PulseIncrementModifier;

        float upShake = 1;
        float leftShake = 1;
        float rightShake = 1;
        float downShake = 1;

        Vector3 centerPosition;
        Vector3 upPosition;
        Vector3 leftPosition;
        Vector3 rightPosition;
        Vector3 downPosition;

        enum Polarity
        {
            Positive,
            Negative
        }

        enum Request
        {
            Up,
            Left,
            Right,
            Down
        }

        void Start()
        {
            transform = gameObject.transform;
            orbs = new RectTransform[100];

            centerImage = Center.GetComponent<Image>();
            upImage = Up.GetComponent<Image>();
            leftImage = Left.GetComponent<Image>();
            rightImage = Right.GetComponent<Image>();
            downImage = Down.GetComponent<Image>();

            upImage.color = Color.blue;
            leftImage.color = Color.yellow;
            rightImage.color = Color.green;
            downImage.color = Color.red;

            centerPosition = Center.position;
            upPosition = Up.position;
            leftPosition = Left.position;
            rightPosition = Right.position;
            downPosition = Down.position;

            currentRequest = RandomRequest();
            centerImage.color = RequestColor(currentRequest);

            var emission = ScoreParticles.emission;
            emission.rateOverTime = 0;

            ScoreParticles.Play();
            timer = GameDuration;
        }

        void Update()
        {
            deltaTime = Time.deltaTime;

            CheckInput();
            UpdateOrbs();
            Juice();

            timer -= Time.deltaTime;
            pulse += Time.deltaTime * score * PulseIncrementModifier;
            timerText.text = string.Empty + (int)timer;

            if (timer <= 0)
            {
                Destroy(this);
                var emission = ScoreParticles.emission;
                emission.rateOverTime = 0;
            }
        }

        void CheckInput()
        {
            if (IsDown("Vertical", Polarity.Positive, ref up_down))
            {
                Answer(Request.Up);
                CreateOrb(Up.position);
            }

            if (IsDown("Vertical", Polarity.Negative, ref down_down))
            {
                Answer(Request.Down);
                CreateOrb(Down.position);
            }

            if (IsDown("Horizontal", Polarity.Negative, ref left_down))
            {
                Answer(Request.Left);
                CreateOrb(Left.position);
            }

            if (IsDown("Horizontal", Polarity.Positive, ref right_down))
            {
                Answer(Request.Right);
                CreateOrb(Right.position);
            }
        }

        bool IsDown(string axisPath, Polarity polarity, ref bool axisDown)
        {
            if (polarity == Polarity.Positive && Input.GetAxisRaw(axisPath) <= 0)
            {
                axisDown = false;
                return false;
            }

            if (polarity == Polarity.Negative && Input.GetAxisRaw(axisPath) >= 0)
            {
                axisDown = false;
                return false;
            }

            if (!axisDown)
            {
                axisDown = true;
                return true;
            }

            return false;
        }

        void CreateOrb(Vector3 position)
        {
            for (int i = 0; i < orbs.Length; i++)
            {
                if (orbs[i] == null)
                {
                    orbs[i] = GameObject.Instantiate(Orb, position, Quaternion.Euler(Vector3.zero), OrbsParent).transform;
                    return;
                }
            }
        }

        void UpdateOrbs()
        {

            foreach (Transform t in orbs)
            {
                if (t == null)
                    continue;

                if (Vector3.Distance(t.position, Center.position) < 1)
                    Destroy(t.gameObject);
                else
                    t.position = Vector3.Lerp(t.position, Center.position, 6 * deltaTime);
            }
        }

        Request RandomRequest()
        {
            return (Request)Random.Range(0, 4);
        }

        Request RandomRequest(Request avoidRequest)
        {
            for (int i = 0; i < 4; i++)
            {
                if (i == 3)
                {
                    if ((int)avoidRequest < 3)
                        return (Request)((int)avoidRequest + 1);
                    else
                        return (Request)1;
                }

                Request newRequest = RandomRequest();
                if (newRequest != avoidRequest)
                    return newRequest;
            }

            return (Request)Random.Range(0, 4);
        }

        Color RequestColor(Request request)
        {
            switch (request)
            {
                case Request.Up:
                    return upImage.color;
                case Request.Left:
                    return leftImage.color;
                case Request.Right:
                    return rightImage.color;
                case Request.Down:
                    return downImage.color;
            }

            return Color.magenta;
        }

        void Answer(Request answer)
        {
            StartShake(answer);

            if (answer == currentRequest)
            {
                // Correct Answer!
                score += 1;

                var main = AnswerParticles.main;
                main.startColor = RequestColor(answer);

                AnswerParticles.Play();
                currentRequest = RandomRequest(currentRequest);
                centerImage.color = RequestColor(currentRequest);
            }
            else
            {
                score -= 1;
            }

            var emission = ScoreParticles.emission;
            emission.rateOverTime = score;
            scoreText.text = string.Empty + score;
        }

        void StartShake(Request request)
        {
            switch (request)
            {
                case Request.Up:
                    upShake = 0;
                    return;
                case Request.Left:
                    leftShake = 0;
                    return;
                case Request.Right:
                    rightShake = 0;
                    return;
                case Request.Down:
                    downShake = 0;
                    return;
            }
        }

        void Juice()
        {
            float scoreModifier = score * 0.1f;

            Center.localScale = Vector3.Slerp(Vector3.one * 0.5f, Vector3.one, (Mathf.Abs(Mathf.Cos(pulse))));
            JuiceLight.intensity = Mathf.Abs(Mathf.Cos(pulse)) * score * JuiceLightModifier;

            Shake();
        }

        void Shake()
        {
            Center.position = new Vector3(
                centerPosition.x + (ShakeCurve.Evaluate(leftShake / ShakeDuration) - ShakeCurve.Evaluate(rightShake / ShakeDuration)) * CenterShakeModifier,
                centerPosition.y + (-ShakeCurve.Evaluate(upShake / ShakeDuration) + ShakeCurve.Evaluate(downShake / ShakeDuration)) * CenterShakeModifier,
                centerPosition.z);

            Up.position = new Vector3(Up.position.x, upPosition.y + ShakeCurve.Evaluate(upShake / ShakeDuration) * ShakeModifier, Up.position.z);
            Left.position = new Vector3(leftPosition.x - ShakeCurve.Evaluate(leftShake / ShakeDuration) * ShakeModifier, Left.position.y, Left.position.z);
            Right.position = new Vector3(rightPosition.x + ShakeCurve.Evaluate(rightShake / ShakeDuration) * ShakeModifier, Right.position.y, Right.position.z);
            Down.position = new Vector3(Down.position.x, downPosition.y - ShakeCurve.Evaluate(downShake / ShakeDuration) * ShakeModifier, Down.position.z);

            upShake = Mathf.Min(upShake + deltaTime / ShakeDuration, 1);
            leftShake = Mathf.Min(leftShake + deltaTime / ShakeDuration, 1);
            rightShake = Mathf.Min(rightShake + deltaTime / ShakeDuration, 1);
            downShake = Mathf.Min(downShake + deltaTime / ShakeDuration, 1);
        }
    }
}
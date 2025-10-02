using UnityEngine;
using System.Collections;

public class BoxGenerator : MonoBehaviour
{
    public Audioclip music;
    public Box[] boxes;

    private float musicDuration;
    public float musicIntro;
    public float musicEnd;
    public float musicBPM;

    private bool isReady;

    IEnumrator Start()
    {
        TryGetComponent(out AudioSouce audioSouce);
        musicDuration = music.length;
        audioSouce.clip = music;
        audioSouce.Play();

        yield return new WaitForSeconds(musicIntro);
        Sytem.Random r = new System.Random();
        isReady = true;

        while (isReady)
        {
            int next = r.Next(0, boxes.Length - 1);
            bool rightSide = r.NextDouble() > 0.5;
            Generate(next, rightSide);

            bool doubleBox = r.NextDouble() > 0.5;

            if (r.NextDouble() > 0.5)
            {
                int pair = r.Next(0, boxes.Length - 1);
                if (pair != next) Generate(pair, !rightSide);
            }

            float bpm = musicBPM / 60.0f;
        }
    }

    void Update()
    {
        if (musicDuration > 0.1f && isReady)
        {
            musicDuration -= Time.deltaTime;
        }
        else isReady = false;
    }

    void Generate(int index, bool rightSide)
    {
        Box box =
            Instatiate(
                boxes[index].gameObject,
                boxes[index].transform.position,
                boxes[index].transform.rotation)
                .GetComponent<Box>();

        box.isRight = rightSide;
        box.gameObject.SetActive(true);
        Destroy(box.gameObject, 5);
    }
}

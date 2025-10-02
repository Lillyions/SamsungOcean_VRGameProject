using UnityEngine;
using System.Collections;

public class BoxGenerator : MonoBehaviour
{
    public AudioClip music;
    public Box[] boxes;

    private float musicDuration;
    public float musicIntro;
    public float musicEnd;
    public float musicBPM;

    private bool isReady;

    IEnumerator Start()
    {
        TryGetComponent(out AudioSource audioSouce);
        musicDuration = music.length;
        audioSouce.clip = music;
        audioSouce.Play();

        yield return new WaitForSeconds(musicIntro);
        System.Random r = new System.Random();
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
            yield return new WaitForSeconds(bpm);
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
            Instantiate(
                boxes[index].gameObject,
                boxes[index].transform.position,
                boxes[index].transform.rotation)
                .GetComponent<Box>();

        box.isRight = rightSide;
        box.gameObject.SetActive(true);
        Destroy(box.gameObject, 5);
    }
}

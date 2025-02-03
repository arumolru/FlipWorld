using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    AudioSource sound;
    Animator anim;

    [SerializeField]
    private SpriteRenderer miniGoal; // 오브젝트의 자식 오브젝트

    public GameManager manager; // 게임 매니저

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // 오브젝트 회전
        if (spriteRenderer.color == Color.white)
        {
            gameObject.transform.Rotate(0, 0, 0.05f);
        }

        else
        {
            gameObject.transform.Rotate(0, 0, 0.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 오브젝트가 플레이어에 닿았을 경우
        if (collision.gameObject.tag == "Player")
        {
            // 두 오브젝트 색 반전
            spriteRenderer.color = new Color(0, 0, 0);
            miniGoal.color = new Color(1, 1, 1);

            // 골인 사운드 출력
            sound.Play();

            // 애니메이션 출력
            anim.SetTrigger("NextLevel");

            // 잠시 후 다음 스테이지로 이동
            Invoke("NextLevel", 3f);
        }
    }

    void NextLevel()
    {
        SceneManager.LoadScene("Stage" + (manager.stageLevel + 1));
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer rbSprite;
    Animator anim;
    CapsuleCollider2D capsuleCollider;

    private int maxSpeed = 5; // 플레이어 이동 속도
    private int jumpForce = 8; // 플레이어 점프력

    [SerializeField]
    private GameManager gameManager; // 게임 매니저
    [SerializeField]
    private GameObject panel; // 패널 오브젝트
    [SerializeField]
    private Image panelImage; // 패널 이미지

    [SerializeField]
    private AudioSource[] GameSound; // 게임 사운드

    private bool isYGravity = false; // Y중력 상황
    private bool isSpaceGravity = false; // 공간 상황

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rbSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        Physics2D.gravity = new Vector2(0, -9.81f);
    }

    private void Update()
    {
        if (!gameManager.isPause)
        {
            // 플레이어가 정지하였을 때 저항값 설정
            Resistance();

            // 플레이어의 방향에 따라 바라보는 방향 설정
            Flip();

            // 플레이어 이동 애니메이션
            MoveAnim();

            // 플레이어 점프
            Jump();

            // 플레이어 반전
            SpaceFlip();

            // RayCast를 통한 더블 점프 방지
            RayCastJump();
        }
    }

    private void FixedUpdate()
    {
        if (!gameManager.isPause)
        {
            // 플레이어 이동
            Move();
        }
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");

        rb.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // 플레이어 최대 속도 설정
        if (rb.linearVelocityX > maxSpeed)
        {
            rb.linearVelocity = new Vector2(maxSpeed, rb.linearVelocityY);
        }
        if (rb.linearVelocityX < -maxSpeed)
        {
            rb.linearVelocity = new Vector2(-maxSpeed, rb.linearVelocityY);
        }
    }

    void Resistance()
    {
        if (Input.GetButtonUp("Horizontal"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.normalized.x, rb.linearVelocityY);
        }
    }

    void Flip()
    {
        // 플레이어가 움직이는 방향으로 flipX 조정
        if (Input.GetButton("Horizontal"))
        {
            rbSprite.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
    }

    void MoveAnim()
    {
        // 플레이어 Run 애니메이션 설정
        if (rb.linearVelocity.normalized.x == 0)
        {
            anim.SetBool("IsRun", false);
        }
        else anim.SetBool("IsRun", true);
    }

    void Jump()
    {
        // 플레이어가 점프 상태가 아닐 때만 점프 할 수 있게 설정
        if (Input.GetButtonDown("Jump") && !anim.GetBool("IsJump"))
        {
            GameSound[0].Play(); // 사운드 재생

            // 플레이어가 반전 상태가 아닐 경우
            if (!rbSprite.flipY)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                anim.SetBool("IsJump", true);
            }

            // 플레이어가 반전 상태일 경우
            else
            {
                rb.AddForce(Vector2.down * jumpForce, ForceMode2D.Impulse);
                anim.SetBool("IsJump", true);
            }
        }
    }

    void RayCastJump()
    {
        // 플레이어가 반전 상태가 아닐 경우
        if(rb.linearVelocityY < 0 && !rbSprite.flipY)
        {
            if(isYGravity)
            {
                Debug.DrawRay(rb.position, Vector3.down, new Color(0, 1, 0));

                RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector3.down, 1, LayerMask.GetMask("GrayMap"));

                if (hit.collider != null)
                {
                    if (hit.distance < 0.5f)
                    {
                        anim.SetBool("IsJump", false);
                    }
                }
            }
            else
            {
                Debug.DrawRay(rb.position, Vector3.down, new Color(0, 1, 0));

                RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector3.down, 1, LayerMask.GetMask("BlackMap"));

                if (hit.collider != null)
                {
                    if (hit.distance < 0.5f)
                    {
                        anim.SetBool("IsJump", false);
                    }
                }
            }
        }

        // 플레이어가 반전 상태일 경우
        else if (rb.linearVelocityY > 0 && rbSprite.flipY)
        {
            if(isYGravity)
            {
                Debug.DrawRay(rb.position, Vector3.up, new Color(0, 1, 0));

                RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector3.up, 1, LayerMask.GetMask("BlackMap"));

                if (hit.collider != null)
                {
                    if (hit.distance < 0.5f)
                    {
                        anim.SetBool("IsJump", false);
                    }
                }
            }
            else
            {
                Debug.DrawRay(rb.position, Vector3.up, new Color(0, 1, 0));

                RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector3.up, 1, LayerMask.GetMask("GrayMap"));

                if (hit.collider != null)
                {
                    if (hit.distance < 0.5f)
                    {
                        anim.SetBool("IsJump", false);
                    }
                }
            }
        }
    }

    void SpaceFlip()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !anim.GetBool("IsJump"))
        {       
            isSpaceGravity = !isSpaceGravity;
            Debug.Log(Physics2D.gravity);

            GameSound[1].Play(); // 사운드 재생

            // 플레이어가 공간 반전 상태일 경우
            if (isSpaceGravity)
            {
                // 플레이어가 중력 반전상태가 아닐 경우
                if(!isYGravity)
                {
                    gameObject.layer = 9; // 플레이어 레이어 변경

                    // Physics2D의 Gravity반전
                    FlipGravity();

                    // 플레이어의 Y위치를 기존 위치에서 +1 올림 
                    Vector2 vec = new Vector2(rb.position.x, rb.position.y);
                    vec.y -= 1;
                    rb.position = vec;

                    // 플레이어 반전 및 색 변경
                    rbSprite.flipY = true;
                    rbSprite.color = new Color(166f / 255f, 166f / 255f, 166f / 255f);
                }

                // 플레이어가 중력 반전 상태일 경우
                else
                {
                    gameObject.layer = 9; // 플레이어 레이어 변경

                    // Physics2D의 Gravity반전
                    FlipGravity();

                    // 플레이어의 Y위치를 기존 위치에서 -1 내림 
                    Vector2 vec = new Vector2(rb.position.x, rb.position.y);
                    vec.y += 1;
                    rb.position = vec;

                    // 플레이어 반전 및 색 변경
                    rbSprite.flipY = false;
                    rbSprite.color = new Color(166f / 255f, 166f / 255f, 166f / 255f);
                }
            }

            // 플레이어가 공간 반전 상태가 아닐 경우
            else
            {
                // 플레이어가 중력 반전상태가 아닐 경우
                if (!isYGravity)
                {
                    gameObject.layer = 8; // 플레이어 레이어 변경

                    // Physics2D의 Gravity반전
                    FlipGravity();

                    // 플레이어의 Y위치를 기존 위치에서 -1 내림 
                    Vector2 vec = new Vector2(rb.position.x, rb.position.y);
                    vec.y += 1;
                    rb.position = vec;

                    // 플레이어 반전 및 색 변경
                    rbSprite.flipY = false;
                    rbSprite.color = new Color(1, 1, 1);
                }

                // 플레이어가 중력 반전상태일 경우
                if (isYGravity)
                {
                    gameObject.layer = 8; // 플레이어 레이어 변경

                    // Physics2D의 Gravity반전
                    FlipGravity();

                    // 플레이어 반전 및 색 변경
                    rbSprite.flipY = true;
                    rbSprite.color = new Color(1, 1, 1f);

                    // 플레이어의 Y위치를 기존 위치에서 +1 올림 
                    Vector2 vec = new Vector2(rb.position.x, rb.position.y);
                    vec.y -= 1;
                    rb.position = vec;
                }
            }
        }
    }

    // 중력 반전
    void FlipGravity()
    {
        Vector2 gravity = Physics2D.gravity;
        Physics2D.gravity = new Vector2(gravity.x, -gravity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어가 Goal에 닿았을 때 비활성화
        if (collision.gameObject.tag == "Goal")
        {
            gameObject.SetActive(false);
        }

        // 플레이어가 Spike에 닿았을 경우
        if (collision.gameObject.tag == "Spike")
        {
            rbSprite.color = new Color(1, 1, 1, 0.4f); // 플레이어 피격 시 반투명화
            // 콜라이더 비활성
            capsuleCollider.enabled = false;
            // 플레이어 컨트롤 불가
            gameManager.isPause = true;
            // 패널 활성화
            panel.SetActive(true);
            // 사망 시 점프 효과
            rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            // 게임 오버 사운드 출력
            GameSound[2].Play();
            // 재시작
            StartCoroutine(Retry());
        }

        // 플레이어가 MapWallX에 닿았을 경우
        /*if (collision.gameObject.tag == "MapWallX")
        {
            // 플레이어의 X위치를 반전
            Vector2 vec = new Vector2(rb.position.x, rb.position.y);
            vec.x *= -1;
            rb.position = vec;
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어가 MapWallY에 닿앗을 경우
        if (collision.gameObject.tag == "MapWallY")
        {
            // 플레이어의 Y위치를 반전
            Vector2 vec = new Vector2(rb.position.x, rb.position.y);
            vec.y *= -1;
            rb.position = vec;
        }

        // 플레이어가 MainCoin에 닿았을 경우
        if (collision.gameObject.tag == "MainCoin")
        {
            GameSound[3].Play(); // 사운드 재생

            // CoinBlock이라는 태그를 가진 모든 오브젝트 찾기
            GameObject[] coinBlocks = GameObject.FindGameObjectsWithTag("CoinBlock");

            // CoinBlock 태그를 가진 모든 오브젝트의 Layer변경
            foreach(GameObject coinBlock in coinBlocks)
            {
                SpriteRenderer coinBlockRenderer = coinBlock.GetComponent<SpriteRenderer>();

                // 코인 블럭의 색이 Black일 경우
                if (coinBlock.layer == 6)
                {
                    // 색상 변경
                    coinBlock.layer = 7;
                    coinBlockRenderer.color = new Color(137f / 255f, 137f / 255f, 137f / 255f, 1);
                }

                // 코인 블럭의 색이 Gray일 경우
                else if (coinBlock.layer == 7)
                {
                    // 색상 변경
                    coinBlock.layer = 6;
                    coinBlockRenderer.color = new Color(0, 0, 0, 1);
                }
            }
        }

        // 플레이어가 SilverCoin에 닿았을 경우
        if (collision.gameObject.tag == "SilverCoin")
        {
            GameSound[3].Play(); // 사운드 재생

            // CoinBlock이라는 태그를 가진 모든 오브젝트 찾기
            GameObject[] coinBlocks = GameObject.FindGameObjectsWithTag("SilverCoinBlock");

            // CoinBlock 태그를 가진 모든 오브젝트의 Layer변경
            foreach (GameObject coinBlock in coinBlocks)
            {
                SpriteRenderer coinBlockRenderer = coinBlock.GetComponent<SpriteRenderer>();

                // 코인 블럭의 색이 Black일 경우
                if (coinBlock.layer == 6)
                {
                    // 색상 변경
                    coinBlock.layer = 7;
                    coinBlockRenderer.color = new Color(137f / 255f, 137f / 255f, 137f / 255f, 1);
                }

                // 코인 블럭의 색이 Gray일 경우
                else if (coinBlock.layer == 7)
                {
                    // 색상 변경
                    coinBlock.layer = 6;
                    coinBlockRenderer.color = new Color(0, 0, 0, 1);
                }
            }
        }

        // 플레이어가 GravityFlip에 닿았을 경우
        if (collision.gameObject.tag == "GravityFlip")
        {
            isYGravity = true;

            // Physics2D의 Gravity반전
            FlipGravity();

            rbSprite.flipY = true;

            Debug.Log(isYGravity);
        }
    }

    IEnumerator Retry()
    {
        float loadTime = 0; // 로딩 시간
        while (loadTime < 2.0f) // 알파값이 1이 될 때까지 반복
        {
            loadTime += 0.01f;
            yield return new WaitForSeconds(0.01f); // 0.01초마다 실행
            panelImage.color = new Color(0, 0, 0, loadTime); // 해당 패널의 투명도 조절
        }

        if (loadTime > 1.5f)
        {
            SceneManager.LoadScene("Stage" + gameManager.stageLevel); // 메인 화면으로 이동
        }
    }
}

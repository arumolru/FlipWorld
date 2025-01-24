using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer rbSprite;
    Animator anim;

    private int maxSpeed = 5; // 플레이어 이동 속도
    private int jumpForce = 8; // 플레이어 점프력

    [SerializeField]
    private GameManager gameManager; // 게임 매니저

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rbSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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
        }
    }

    private void FixedUpdate()
    {
        if (!gameManager.isPause)
        {
            // 플레이어 이동
            Move();

            // RayCast를 통한 더블 점프 방지
            RayCastJump();
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
            // 플레이어가 반전 상태가 아닐 경우
            if (gameObject.layer == 8)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                anim.SetBool("IsJump", true);
            }

            // 플레이어가 반전 상태일 경우
            else if (gameObject.layer == 9)
            {
                rb.AddForce(Vector2.down * jumpForce, ForceMode2D.Impulse);
                anim.SetBool("IsJump", true);
            }
        }
    }

    void RayCastJump()
    {
        // 플레이어가 반전 상태가 아닐 경우
        if(rb.linearVelocityY < 0 && gameObject.layer == 8)
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

        // 플레이어가 반전 상태일 경우
        else if (rb.linearVelocityY > 0 && gameObject.layer == 9)
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

    void SpaceFlip()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !anim.GetBool("IsJump"))
        {
            // 플레이어가 반전 상태일 경우
            if (rbSprite.flipY)
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

            // 플레이어가 반전 상태가 아닐 경우
            else
            {
                gameObject.layer = 9; // 플레이어 레이어 변경

                // Physics2D의 Gravity반전
                FlipGravity();

                // 플레이어 반전 및 색 변경
                rbSprite.flipY = true;
                rbSprite.color = new Color(166f / 255f, 166f / 255f, 166f / 255f);

                // 플레이어의 Y위치를 기존 위치에서 +1 올림 
                Vector2 vec = new Vector2(rb.position.x, rb.position.y);
                vec.y -= 1;
                rb.position = vec;
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
    }
}

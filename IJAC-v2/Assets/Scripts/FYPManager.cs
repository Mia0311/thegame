using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Video;

public class FYPManager : MonoBehaviour
{
    public GameObject postPrefab;
    public Transform contentPanel;
    public Sprite[] images;
    public VideoClip[] videos;
    public TMP_FontAsset commentFont;
    private List<PostData> postList = new List<PostData> ();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        LoadPosts();
    }

    public void LoadPosts()
    {
        string[] usernames = { "Alice", "Bob", "Charlie", "David", "Eve" };

        for (int i = 0; i < 10; i++)
        {
            bool isVideoPost = Random.value > 0.5f;
            Debug.Log($"Iteration {i}: Random isVideoPost = {isVideoPost}");

            PostData post = new PostData
            {
                username = usernames[Random.Range(0, usernames.Length)],
                isVideo = isVideoPost,
                image = isVideoPost ? null : images[Random.Range(0, images.Length)],
                video = isVideoPost ? videos[Random.Range(0, videos.Length)] : null,
                likes = Random.Range(0, 1000),
                comments = new List<string>
                { "Awesome post!",
                    "Love this!",
                    "Amazing content! Keep it up!",
                    "This is so cool!",
                    "Great shot!",
                    "So inspiring!",
                    "I need this in my life!",
                    "Incredible! How did you do this?",
                    "Love the vibes in this one!",
                    "So creative!",
                    "You always post the best content!",
                    "Wow, just wow!",
                    "This made my day!",
                    "I can't stop watching this!",
                    "The quality is next level!",
                    "You're so talented!",
                    "This is absolutely stunning!",
                    "The colors in this are perfect!",
                    "I showed this to my friends, they loved it too!",
                    "Epic content!",
                    "I've watched this 5 times already!",
                    "This should go viral!",
                    "I'm obsessed with this!",
                    "Definitely one of your best posts!",
                    "So relatable!",
                    "Love the energy in this!",
                    "The detail is amazing!",
                    "You always bring something new!",
                    "Can't wait for more posts like this!",
                    "This deserves way more likes!",
                    "Legendary content!",
                    "You never miss!",
                    "This gave me chills!",
                    "Teach me your ways!",
                    "You should post more often!",
                    "The background music is perfect!",
                    "Literally the best thing I've seen today!",
                    "This needs to be on my feed every day!",
                    "So underrated!",
                    "This is straight-up art!",
                    "You always make my feed better!",
                    "I aspire to create like this one day!",
                    "Such a wholesome post!",
                    "Your creativity has no limits!",
                    "I feel so motivated after watching this!",
                    "You're an inspiration!",
                    "Can't wait to see what you post next!"
                }
            };

            Debug.Log($"Iteration {i}: Created PostData - isVideo = {post.isVideo}, image = {post.image}, video = {post.video}");

            postList.Add(post);
            CreatePostUI(post);
        }
    }

    public void CreatePostUI (PostData post)
    {
        GameObject newPost = Instantiate(postPrefab, contentPanel);

        Debug.Log($"Creating UI for post - isVideo = {post.isVideo}");

        TextMeshProUGUI usernameText = newPost.transform.Find("Username").GetComponent<TextMeshProUGUI>();
        Image postImage = newPost.transform.Find("PostImage").GetComponent<Image>();
        RawImage postVideo = newPost.transform.Find("PostVideoRaw").GetComponent<RawImage>();
        VideoPlayer videoPlayer = newPost.transform.Find("PostVideoRaw/PostVideoPlayer").GetComponent<VideoPlayer>();

        Debug.Log($"Setting username: {post.username}");

        usernameText.text = post.username;

        if (post.isVideo)
        {
            Debug.Log("Processing as video post");
            postImage.gameObject.SetActive(false);
            postVideo.gameObject.SetActive(true);
            videoPlayer.clip = post.video;
            videoPlayer.SetDirectAudioMute(0, true);
            videoPlayer.Play();
        } 
        else
        {
            Debug.Log("Processing as image post");
            postImage.gameObject.SetActive(true);
            postVideo.gameObject.SetActive(false);
            postImage.sprite = post.image;
        }

        newPost.transform.Find("LikeCount").GetComponent<TextMeshProUGUI>().text = post.likes.ToString();

        Transform commentSection = newPost.transform.Find("CommentsSection/Viewport/Content");

        if (commentSection == null)
        {
            Debug.LogError("Comment Section NOT FOUND! Check your prefab structure.");
            return;
        }

        foreach (string comment in post.comments)
        {
            Debug.Log($"Creating comment: {comment}"); // Debug message

            GameObject commentText = new GameObject("Comment");
            TextMeshProUGUI textComp = commentText.AddComponent<TextMeshProUGUI>();

            textComp.text = comment;
            textComp.fontSize = 30;
            textComp.color = Color.white;
            textComp.enableWordWrapping = true;

            if (commentFont != null)
            {
                textComp.font = commentFont;
            }
            else
            {
                Debug.LogWarning("No font assigned! Using default TMP font.");
            }

            RectTransform rectTransform = commentText.GetComponent<RectTransform>();
            rectTransform.SetParent(commentSection, false);

            rectTransform.sizeDelta = new Vector2(845, rectTransform.sizeDelta.y); 
            rectTransform.anchorMin = new Vector2(0, 1); 
            rectTransform.anchorMax = new Vector2(0, 1);
            rectTransform.pivot = new Vector2(0, 1);
        }
    }

}

[System.Serializable]
public class PostData
{
    public string username;
    public Sprite image;
    public VideoClip video;
    public bool isVideo; 
    public int likes;
    public List<string> comments; 
}

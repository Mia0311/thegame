using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Video;

[System.Serializable]
public class CommentData
{
    public string commentUser;
    public string commentText;
}

[System.Serializable]
public class PostData
{
    public string username;
    public string caption;
    //public Sprite image;
    //public VideoClip video;
    public string imageName;
    public string videoName;
    public bool isVideo;
    public int likes;
    public List<CommentData> comments;
}

[System.Serializable]
public class PostDataWrapper
{
    public List<PostData> posts;
}

public class FYPManager : MonoBehaviour
{
    public GameObject postPrefab;
    public Transform contentPanel;
    //public Sprite[] images;
    //public VideoClip[] videos;
    public TMP_FontAsset commentFont;

    private List<PostData> postList = new List<PostData> ();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        LoadPosts();
    }

    /*public void LoadPosts()
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
    } */

    public void LoadPosts ()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("posts");

        if (jsonFile == null)
        {
            Debug.LogError("posts.json not found in Resources folder!");
            return;
        }

        postList = JsonUtility.FromJson<PostDataWrapper>("{\"posts\":" + jsonFile.text + "}").posts;

        foreach (PostData post in postList)
        {
            CreatePostUI(post);
        }
    }

    public void CreatePostUI (PostData post)
    {
        GameObject newPost = Instantiate(postPrefab, contentPanel);

        //Debug.Log($"Creating UI for post - isVideo = {post.isVideo}");

        TextMeshProUGUI usernameText = newPost.transform.Find("UsernameBackground/Username").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI captionText = newPost.transform.Find("CaptionBackground/PostCaption").GetComponent<TextMeshProUGUI>();
        Image postImage = newPost.transform.Find("PostImage").GetComponent<Image>();
        RawImage postVideo = newPost.transform.Find("PostVideoRaw").GetComponent<RawImage>();
        VideoPlayer videoPlayer = newPost.transform.Find("PostVideoRaw/PostVideoPlayer").GetComponent<VideoPlayer>();
        TextMeshProUGUI likeCountText = newPost.transform.Find("LikeCount").GetComponent<TextMeshProUGUI>();

        //Debug.Log($"Setting username: {post.username}");

        usernameText.text = post.username;
        captionText.text = post.caption;

        if (post.isVideo)
        {
            //Debug.Log("Processing as video post");
            postImage.gameObject.SetActive(false);
            postVideo.gameObject.SetActive(true);
            /*videoPlayer.clip = post.video;
            videoPlayer.SetDirectAudioMute(0, true);
            videoPlayer.Play();*/

            VideoClip video = Resources.Load<VideoClip>(post.videoName);
            if (video != null)
            {
                videoPlayer.clip = video;
                videoPlayer.SetDirectAudioMute(0, true);
                videoPlayer.Play();
            } 
            else
            {
                Debug.LogWarning($"Video '{post.videoName}' not found in Resources");
            }
        } 
        else
        {
            //Debug.Log("Processing as image post");
            postImage.gameObject.SetActive(true);
            postVideo.gameObject.SetActive(false);
            //postImage.sprite = post.image;

            Sprite image = Resources.Load<Sprite>(post.imageName);
            if (image != null)
            {
                postImage.sprite = image;
            }
            else
            {
                Debug.LogWarning($"Image '{post.imageName}' not found in Resources");
            }
        }

        //newPost.transform.Find("LikeCount").GetComponent<TextMeshProUGUI>().text = post.likes.ToString();

        Button upvoteButton = newPost.transform.Find("UpVoteButton").GetComponent<Button>();
        Button downvoteButton = newPost.transform.Find("DownVoteButton").GetComponent<Button>();

        bool hasUpVoted = false;
        bool hasDownVoted = false;
        int currentLikes = post.likes;
        likeCountText.text = currentLikes.ToString();

        upvoteButton.onClick.AddListener(() =>
        {
            if (!hasUpVoted)
            {
                if (hasDownVoted)
                {
                    currentLikes += 2; 
                    hasDownVoted = false;
                } 
                else
                {
                    currentLikes += 1;
                }

                hasUpVoted = true;
                upvoteButton.interactable = false;
                downvoteButton.interactable = true;
                likeCountText.text = currentLikes.ToString();
            }
        });

        downvoteButton.onClick.AddListener(() =>
        {
            if (!hasDownVoted)
            {
                if (hasUpVoted)
                {
                    currentLikes -= 2; 
                    hasUpVoted = false;
                }
                else
                {
                    currentLikes = Mathf.Max(0, currentLikes - 1);
                }
                
                hasDownVoted = true;
                downvoteButton.interactable = false;
                upvoteButton.interactable = true;
                likeCountText.text = currentLikes.ToString();
            }
        });

        Button postReportButton = newPost.transform.Find("ReportButton").GetComponent<Button>();
        Transform reportPopUpTrans = newPost.transform.Find("ReportPopUp");
        GameObject localReportPopUpPanel = reportPopUpTrans.gameObject;

        postReportButton.onClick.AddListener(() =>
        {
            SetupReportPopup(localReportPopUpPanel, post.username, post.caption);
        });

        Transform commentSection = newPost.transform.Find("CommentsSection/Viewport/Content");
        GameObject commentPrefab = Resources.Load<GameObject>("Prefabs/CommentsPanel");

        /* foreach (string comment in post.comments)
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
        }*/

        foreach (CommentData comment in post.comments)
        {
            GameObject commentGO = Instantiate(commentPrefab, commentSection);

            TextMeshProUGUI commentUsernameText = commentGO.transform.Find("SingleComment/CommentUser").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI commentText = commentGO.transform.Find("SingleComment/CommentText").GetComponent<TextMeshProUGUI>();
            Button likeBtn = commentGO.transform.Find("SingleComment/LikeButton").GetComponent<Button>();
            Button dislikeBtn = commentGO.transform.Find("SingleComment/DislikeButton").GetComponent<Button>();
            Button reportBtn = commentGO.transform.Find("SingleComment/ReportButton").GetComponent<Button>();
            /*Transform commentPopUpTrans = commentGO.transform.Find("SingleComment/ReportPopUp");
            GameObject commentPopUp = commentPopUpTrans?.gameObject;*/

            commentUsernameText.text = comment.commentUser;
            commentText.text = comment.commentText;

            int localLikes = 0;

            likeBtn.onClick.AddListener (() =>
            {
                localLikes++;
                Debug.Log($"Comment liked by {comment.commentUser}. Total likes: {localLikes}");
            }); 
            
            dislikeBtn.onClick.AddListener (() =>
            {
                localLikes = Mathf.Max(0, localLikes - 1);
                Debug.Log($"Comment disliked by {comment.commentUser}. Total likes: {localLikes}");
            }); 
            
            reportBtn.onClick.AddListener (() =>
            {
                Debug.Log($"Comment by {comment.commentUser} reported.");
                //SetupReportPopup(commentPopUp, comment.commentUser, comment.commentText);
            }); 
        }

        TMP_InputField commentInput = newPost.transform.Find("CommentInput").GetComponent<TMP_InputField>();
        Button submitCommentBtn = newPost.transform.Find("CommentSubmitButton").GetComponent<Button>();

        submitCommentBtn.onClick.AddListener(() =>
        {
            string newCommentText = commentInput.text;

            if (!string.IsNullOrEmpty(newCommentText.Trim()))
            {
                CommentData newComment = new CommentData
                {
                    commentUser = "You",
                    commentText = newCommentText
                };

                post.comments.Add(newComment);

                GameObject commentGO = Instantiate(commentPrefab, commentSection);

                TextMeshProUGUI commentPlayerText = commentGO.transform.Find("SingleComment/CommentUser").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI commentText = commentGO.transform.Find("SingleComment/CommentText").GetComponent<TextMeshProUGUI>();
                Button likeBtn = commentGO.transform.Find("SingleComment/LikeButton").GetComponent<Button>();
                Button dislikeBtn = commentGO.transform.Find("SingleComment/DislikeButton").GetComponent<Button>();
                Button reportBtn = commentGO.transform.Find("SingleComment/ReportButton").GetComponent<Button>();
                /*Transform commentPopUpTrans = commentGO.transform.Find("SingleComment/ReportPopUp");
                GameObject commentPopUp = commentPopUpTrans?.gameObject;*/

                commentPlayerText.text = newComment.commentUser;
                commentText.text = newComment.commentText;

                int localLikes = 0;

                likeBtn.onClick.AddListener(() =>
                {
                    localLikes++;
                    Debug.Log($"Comment liked by {newComment.commentUser}. Total likes: {localLikes}");
                });

                dislikeBtn.onClick.AddListener(() =>
                {
                    localLikes = Mathf.Max(0, localLikes - 1);
                    Debug.Log($"Comment disliked by {newComment.commentUser}. Total likes: {localLikes}");
                });

                reportBtn.onClick.AddListener(() =>
                {
                    Debug.Log($"Comment by {newComment.commentUser} reported.");
                    //SetupReportPopup(commentPopUp, newComment.commentUser, newComment.commentText);
                });

                commentInput.text = "";
            }
        });
    }

    void SetupReportPopup(GameObject popup, string username, string text)
    {
        TMP_Dropdown reasonDropdown = popup.transform.Find("Reasons").GetComponent<TMP_Dropdown>();
        Button confirmButton = popup.transform.Find("ConfirmButton").GetComponent<Button>();
        Button cancelButton = popup.transform.Find("CancelButton").GetComponent<Button>();

        popup.SetActive(true);

        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(() =>
        {
            string reason = reasonDropdown.options[reasonDropdown.value].text;
            Debug.Log($"Reported by: {username}\nText: {text}\nReason: {reason}");
            popup.SetActive(false);
        });

        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(() =>
        {
            popup.SetActive(false);
        });
    }


}

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
    public TMP_FontAsset commentFont;
    public GameObject reportPopUpPrefab;

    private List<PostData> postList = new List<PostData>();

    void Start()
    {
        LoadPosts();
    }

    public void LoadPosts()
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

    public void CreatePostUI(PostData post)
    {
        GameObject newPost = Instantiate(postPrefab, contentPanel);

        // Set up post fields (username, caption, etc.)
        SetText(newPost, "UsernameBackground/Username", post.username);
        SetText(newPost, "CaptionBackground/PostCaption", post.caption);

        // Set image/video
        SetImageOrVideo(newPost, post);

        // Set like count and voting buttons
        SetLikeButtonActions(newPost, post);

        // Set up report button
        SetReportButton(newPost, post);

        // Set up comments section
        SetUpCommentsSection(newPost, post);
    }

    private void SetText(GameObject parent, string path, string text)
    {
        TextMeshProUGUI textComp = parent.transform.Find(path).GetComponent<TextMeshProUGUI>();
        textComp.text = text;
    }

    private void SetImageOrVideo(GameObject newPost, PostData post)
    {
        Image postImage = newPost.transform.Find("PostImage").GetComponent<Image>();
        RawImage postVideo = newPost.transform.Find("PostVideoRaw").GetComponent<RawImage>();
        VideoPlayer videoPlayer = newPost.transform.Find("PostVideoRaw/PostVideoPlayer").GetComponent<VideoPlayer>();

        if (post.isVideo)
        {
            postImage.gameObject.SetActive(false);
            postVideo.gameObject.SetActive(true);
            LoadVideo(post.videoName, videoPlayer);
        }
        else
        {
            postImage.gameObject.SetActive(true);
            postVideo.gameObject.SetActive(false);
            LoadImage(post.imageName, postImage);
        }
    }

    private void LoadVideo(string videoName, VideoPlayer videoPlayer)
    {
        VideoClip video = Resources.Load<VideoClip>(videoName);
        if (video != null)
        {
            videoPlayer.clip = video;
            videoPlayer.SetDirectAudioMute(0, true);
            videoPlayer.Play();
        }
        else
        {
            Debug.LogWarning($"Video '{videoName}' not found in Resources");
        }
    }

    private void LoadImage(string imageName, Image postImage)
    {
        Sprite image = Resources.Load<Sprite>(imageName);
        if (image != null)
        {
            postImage.sprite = image;
        }
        else
        {
            Debug.LogWarning($"Image '{imageName}' not found in Resources");
        }
    }

    private void SetLikeButtonActions(GameObject newPost, PostData post)
    {
        TextMeshProUGUI likeCountText = newPost.transform.Find("LikeCount").GetComponent<TextMeshProUGUI>();
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
    }

    private void SetReportButton(GameObject newPost, PostData post)
    {
        Button postReportButton = newPost.transform.Find("ReportButton").GetComponent<Button>();
        postReportButton.onClick.AddListener(() =>
        {
            GameObject reportPostUI = Instantiate(reportPopUpPrefab, contentPanel.parent);
            reportPostUI.transform.SetAsLastSibling();

            Button confirmButton = reportPostUI.transform.Find("ConfirmButton").GetComponent<Button>();
            Button cancelButton = reportPostUI.transform.Find("CancelButton").GetComponent<Button>();

            confirmButton.onClick.AddListener(() =>
            {
                Debug.Log($"Post by {post.username} reported for reason: {GetSelectedReason(reportPostUI)}");
                Destroy(reportPostUI);
            });

            cancelButton.onClick.AddListener(() =>
            {
                Destroy(reportPostUI);
            });
        });
    }

    private void SetUpCommentsSection(GameObject newPost, PostData post)
    {
        Transform commentSection = newPost.transform.Find("CommentsSection/Viewport/Content");
        GameObject commentPrefab = Resources.Load<GameObject>("Prefabs/CommentsPanel");

        foreach (CommentData comment in post.comments)
        {
            GameObject commentGO = Instantiate(commentPrefab, commentSection);

            TextMeshProUGUI commentUsernameText = commentGO.transform.Find("SingleComment/CommentUser").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI commentText = commentGO.transform.Find("SingleComment/CommentText").GetComponent<TextMeshProUGUI>();
            Button likeBtn = commentGO.transform.Find("SingleComment/LikeButton").GetComponent<Button>();
            Button dislikeBtn = commentGO.transform.Find("SingleComment/DislikeButton").GetComponent<Button>();
            Button reportBtn = commentGO.transform.Find("SingleComment/ReportButton").GetComponent<Button>();

            commentUsernameText.text = comment.commentUser;
            commentText.text = comment.commentText;

            int localLikes = 0;

            likeBtn.onClick.AddListener(() =>
            {
                localLikes++;
                Debug.Log($"Comment liked by {comment.commentUser}. Total likes: {localLikes}");
            });

            dislikeBtn.onClick.AddListener(() =>
            {
                localLikes = Mathf.Max(0, localLikes - 1);
                Debug.Log($"Comment disliked by {comment.commentUser}. Total likes: {localLikes}");
            });

            reportBtn.onClick.AddListener(() =>
            {
                GameObject reportCommentUI = Instantiate(reportPopUpPrefab, contentPanel.parent);
                reportCommentUI.transform.SetAsLastSibling();

                Button confirmBtn = reportCommentUI.transform.Find("ConfirmButton").GetComponent<Button>();
                Button cancelBtn = reportCommentUI.transform.Find("CancelButton").GetComponent<Button>();

                confirmBtn.onClick.AddListener(() =>
                {
                    Debug.Log($"Comment by {comment.commentUser} reported for reason: {GetSelectedReason(reportCommentUI)}");
                    Destroy(reportCommentUI);
                });

                cancelBtn.onClick.AddListener(() =>
                {
                    Destroy(reportCommentUI);
                });
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
                CreateCommentUI(newPost, newComment);
                commentInput.text = "";
            }
        });
    }

    private void CreateCommentUI(GameObject postUI, CommentData comment)
    {
        Transform commentSection = postUI.transform.Find("CommentsSection/Viewport/Content");
        GameObject commentPrefab = Resources.Load<GameObject>("Prefabs/CommentsPanel");

        GameObject commentGO = Instantiate(commentPrefab, commentSection);

        TextMeshProUGUI commentUsernameText = commentGO.transform.Find("SingleComment/CommentUser").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI commentText = commentGO.transform.Find("SingleComment/CommentText").GetComponent<TextMeshProUGUI>();
        Button likeBtn = commentGO.transform.Find("SingleComment/LikeButton").GetComponent<Button>();
        Button dislikeBtn = commentGO.transform.Find("SingleComment/DislikeButton").GetComponent<Button>();
        Button reportBtn = commentGO.transform.Find("SingleComment/ReportButton").GetComponent<Button>();

        commentUsernameText.text = comment.commentUser;
        commentText.text = comment.commentText;

        int localLikes = 0;

        likeBtn.onClick.AddListener(() =>
        {
            localLikes++;
            Debug.Log($"Comment liked by {comment.commentUser}. Total likes: {localLikes}");
        });

        dislikeBtn.onClick.AddListener(() =>
        {
            localLikes = Mathf.Max(0, localLikes - 1);
            Debug.Log($"Comment disliked by {comment.commentUser}. Total likes: {localLikes}");
        });

        reportBtn.onClick.AddListener(() =>
        {
            GameObject reportCommentUI = Instantiate(reportPopUpPrefab, contentPanel.parent);
            reportCommentUI.transform.SetAsLastSibling();

            Button confirmBtn = reportCommentUI.transform.Find("ConfirmButton").GetComponent<Button>();
            Button cancelBtn = reportCommentUI.transform.Find("CancelButton").GetComponent<Button>();

            confirmBtn.onClick.AddListener(() =>
            {
                Debug.Log($"Comment by {comment.commentUser} reported for reason: {GetSelectedReason(reportCommentUI)}");
                Destroy(reportCommentUI);
            });

            cancelBtn.onClick.AddListener(() =>
            {
                Destroy(reportCommentUI);
            });
        });
    }

    private string GetSelectedReason(GameObject popup)
    {
        TMP_Dropdown reasonsDropdown = popup.transform.Find("Reasons").GetComponent<TMP_Dropdown>();
        if (reasonsDropdown.options.Count > reasonsDropdown.value)
        {
            return reasonsDropdown.options[reasonsDropdown.value].text;
        }
        else
        {
            Debug.LogWarning("Invalid dropdown selection.");
            return "Unspecified";
        }
    }
}

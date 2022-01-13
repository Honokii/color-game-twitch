namespace WIS.TwitchComponent {
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    using TwitchLib.Unity;
    using TwitchLib.Api.V5.Models.Videos;
    using TwitchLib.Api.V5.Models.Channels;
    using TwitchLib.Api.Helix.Models.Clips.GetClips;
    using TwitchLib.Api.Helix.Models.Users.GetUsers;

    public class TwitchApiComponent : MonoBehaviour
    {
        private TwitchApiProperty _ApiProperty = null;
        private Api _TwitchApi = null;
        private Api TwitchApi {
            get {
                if (_TwitchApi == null) {
                    _TwitchApi = new Api();
                }

                return _TwitchApi;
            }
        }

        #region Public Methods

        public void InitializeManager(string streamerClientId, string botAccessToken) {
            if (_ApiProperty == null) {
                _ApiProperty = new TwitchApiProperty();
            }

            _ApiProperty.StreamerClientId = streamerClientId;
            _ApiProperty.BotAccessToken = botAccessToken;

            TwitchApi.Settings.ClientId = streamerClientId;
            TwitchApi.Settings.AccessToken = botAccessToken;
        }

        public IEnumerator GetTwitchChannelId(string userName, Action<string> onComplete) {
            GetUsersResponse getUsersResponse = null;
            yield return TwitchApi.InvokeAsync(TwitchApi.Helix.Users.GetUsersAsync(logins: new List<string>{userName}), (response) => getUsersResponse = response);
            onComplete(getUsersResponse.Users[0].Id);
        }

        public IEnumerator GetTwitchChannelVideosByUserName(string userName, Action<string[]> onComplete) {
            string channelId = null;
            yield return GetTwitchChannelId(userName, (result) => channelId = result);

            ChannelVideos channelVideos = null;
            yield return TwitchApi.InvokeAsync(TwitchApi.V5.Channels.GetChannelVideosAsync(channelId), (response) => channelVideos = response);
            if (onComplete != null) {
                onComplete(GetVideoUrls(channelVideos.Videos));
            }
        }

        public IEnumerator GetTwitchChannelClipsByUserName(string userName, Action<string[]> onComplete) {
            string channelId = null;
            yield return GetTwitchChannelId(userName, (result) => channelId = result);

            GetClipsResponse getClipsResponse = null;
            yield return TwitchApi.InvokeAsync(TwitchApi.Helix.Clips.GetClipsAsync(broadcasterId: channelId), (result) => getClipsResponse = result);

            // if (onComplete != null) {
            //     onComplete(GetClipUrls(getClipsResponse.Clips));
            // }

            if (getClipsResponse.Clips.Length == 0) {
                onComplete(null);
                // Debug.Log("no clip found");
            } else {
                int randomIndex = UnityEngine.Random.Range(0, getClipsResponse.Clips.Length - 1);
                string clipId = getClipsResponse.Clips[randomIndex].Id;
                // Debug.Log(clipId);
                yield return GetTwitchChannelVideoByVideoId(clipId, onComplete);
            }
        }

        private IEnumerator GetTwitchChannelVideoByVideoId(string videoId, Action<string[]> onComplete) {
            Video video = null;
            yield return TwitchApi.InvokeAsync(TwitchApi.V5.Videos.GetVideoAsync(videoId), (result) => video = result);
            // Debug.Log(video.Url);
            if (onComplete != null) {
                onComplete(null);
            }
        }

        #endregion

        #region  Private Methods

        private string[] GetVideoUrls(Video[] videos) {
            if (videos == null || videos.Length == 0) {
                return null;
            }

            List<string> videoUrls = new List<string>();
            foreach (var video in videos) {
                videoUrls.Add(video.Url);
            }

            return videoUrls.ToArray();
        }

        private string[] GetClipUrls(Clip[] clips) {
            if (clips == null || clips.Length == 0) {
                return null;
            }

            List<string> clipUrls = new List<string>();
            foreach (var clip in clips) {
                clipUrls.Add(clip.EmbedUrl);
            }

            return clipUrls.ToArray();
        }

        #endregion

        public class TwitchApiProperty {
            public string StreamerClientId;
            public string BotAccessToken;
        }
    }
}
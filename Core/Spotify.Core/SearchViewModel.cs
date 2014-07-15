using System;
using RestSharp;
using GalaSoft.MvvmLight;
using System.Collections.Generic;

namespace Spotify.Core
{
  /// <summary>
  /// View Model to search something on Spotify 
  /// </summary>
  public class SearchViewModel : ViewModelBaseExtended
  {
    const string BaseUrl = "https://api.spotify.com/";
    const string ClientID = "yourclientid";
    const string ClientSecret = "yourclientsecret";

    RestClient m_restClient;
    RestRequestAsyncHandle m_requestHandle;

    public SearchViewModel()
    {
      m_restClient = new RestClient(BaseUrl);

    }

    public void Search(string queryString)
    {
      if (m_requestHandle != null)
      {
        m_requestHandle.Abort();
      }

      var request = new RestRequest("/v1/search", Method.GET);
      request.AddParameter("client_id", ClientID);
      request.AddParameter("client_secret", ClientSecret);
      request.AddParameter("q", queryString);
//      request.AddParameter("type", "artist,album,track");
      request.AddParameter("type", "track");
      request.AddParameter("limit", 20);
      request.AddParameter("offset", 0);

      m_requestHandle = m_restClient.ExecuteAsync<RootObject>(request, response => {
        TrackList = response.Data.tracks.items;
      });
        
    }

    public List<Item> m_trackList;
    public List<Item> TrackList
    {
      get
      {
        return m_trackList;
      }
      set
      {
        m_trackList = value;
        RaisePropertyChanged("TrackList");
      }
    }

  }
}


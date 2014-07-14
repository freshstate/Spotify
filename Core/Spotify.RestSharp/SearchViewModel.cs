using System;
using RestSharp;
using GalaSoft.MvvmLight;
using System.Collections.Generic;

namespace Spotify.RestSharp
{
  public class SearchViewModel : ViewModelBase
  {
    const string BaseUrl = "https://api.spotify.com/";
    const string ClientID = "b0abdbcad3ca4947881242a102997fba";
    const string ClientSecret = "04236a53f2ff418cb9230afeeef05a05";

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
      request.AddParameter("type", "track");
      request.AddParameter("limit", 3);
      request.AddParameter("offset", 0);

      //RestResponse<Track> response = (RestResponse)m_restClient.Execute(request);
      //System.Console.WriteLine(response.Content);

      //m_requestHandle = m_restClient.ExecuteAsync(request, response => {
      //  Console.WriteLine("response = " +response.Content);
      //});

      m_requestHandle = m_restClient.ExecuteAsync<RootObject>(request, response => {
        //Console.WriteLine("response = " +response.Data.tracks);
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


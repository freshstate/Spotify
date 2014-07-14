using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Spotify.RestSharp;
using System.Collections.Generic;

namespace Spotify.Droid
{
  [Activity(Label = "Spotify.Droid", MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : Activity
  {
    SearchViewModel m_searchViewModel;

    private SearchView m_searchView;
    private ListView m_listView;
    ArrayAdapter<String> m_adapter;
     
    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      m_searchViewModel = new SearchViewModel();
      m_searchViewModel.PropertyChanged += HandlePropertyChanged;

      // Set our view from the "main" layout resource
      SetContentView(Resource.Layout.Main);

      m_searchView = (SearchView)FindViewById(Resource.Id.searchView1);
      m_searchView.QueryTextChange += HandleQueryTextChange;

      m_listView = (ListView)FindViewById(Resource.Id.list_view);


    }

    void HandleQueryTextChange (object sender, SearchView.QueryTextChangeEventArgs e)
    {
      if (!string.IsNullOrEmpty(m_searchView.Query))
      {
        m_searchViewModel.Search(m_searchView.Query);
      }
    }

    void HandlePropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "TrackList")
      {
        UpdateTrackList();
      }
    }

    void UpdateTrackList()
    {
      if (m_searchViewModel == null || m_searchViewModel.TrackList == null)
      {
        return;
      }


      List<String> items = new List<string>();
      foreach(var item in m_searchViewModel.TrackList)
      {
        items.Add(item.name);
      }
      m_adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, items.ToArray());
      m_listView.Adapter = m_adapter;
    }

  }
}



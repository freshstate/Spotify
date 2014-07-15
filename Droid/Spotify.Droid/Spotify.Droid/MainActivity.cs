using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Spotify.Core;
using System.Collections.Generic;

namespace Spotify.Droid
{
  [Activity(Label = "Spotify.Droid", MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : Activity
  {
    SearchViewModel m_searchViewModel;

    private SearchView m_searchView;
    private ListView m_listView;
    private List<string> m_values;
    private ArrayAdapter<String> m_adapter;
     
    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      //init extended dispatcher
      Core.ExtendedDispatcher.Dispatcher.Owner = this;

      //Init view model
      m_searchViewModel = new SearchViewModel();
      m_searchViewModel.PropertyChanged += HandlePropertyChanged;

      // Set our view from the "main" layout resource
      SetContentView(Resource.Layout.Main);

      //add event handler on search view
      m_searchView = (SearchView)FindViewById(Resource.Id.searchView1);
      m_searchView.QueryTextSubmit += HandleQueryTextSubmit; ;

      m_listView = (ListView)FindViewById(Resource.Id.list_view);

      //will store the values we get from the web service
      if (m_values == null)
      {
        m_values = new List<string>();
      }

      //init an adapter to display data in the list
      m_adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, m_values);
      m_listView.Adapter = m_adapter;

    }

    /// <summary>
    /// User submitter a search query
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    void HandleQueryTextSubmit (object sender, SearchView.QueryTextSubmitEventArgs e)
    {
      //hide keyboard
      m_searchView.ClearFocus();

      if (!string.IsNullOrEmpty(m_searchView.Query))
      {
        m_searchViewModel.Search(m_searchView.Query);
      }
    }

    /// <summary>
    /// Handles the vm property changed.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    void HandlePropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "TrackList")
      {
          UpdateTrackList();
      }
    }

    /// <summary>
    /// Updates the track list in the list view
    /// </summary>
    void UpdateTrackList()
    {
      if (m_searchViewModel == null || m_searchViewModel.TrackList == null)
      {
        return;
      }

      m_values = new List<String>();
      foreach (var item in m_searchViewModel.TrackList)
      {
        m_values.Add(item.name);
      }

      m_adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, m_values.ToArray());
      m_listView.Adapter = m_adapter;
    
    }
  }
}



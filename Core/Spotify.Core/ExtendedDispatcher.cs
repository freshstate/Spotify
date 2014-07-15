using System;

namespace Spotify.Core
{
  public class ExtendedDispatcher 
  {
#if MONOTOUCH
    public NSObject Owner {get; set;}
#elif MONODROID
    public Android.App.Activity Owner {get; set;}
#endif

    private static ExtendedDispatcher m_dispatcher;

    public static ExtendedDispatcher Dispatcher
    {
      get 
      {
        if (m_dispatcher == null)
        {
          m_dispatcher = new ExtendedDispatcher();
        }
        return m_dispatcher;
      }
    }

    public void BeginInvoke (Action action)
    {
      if (Owner != null)
      {
#if MONOTOUCH
        owner.BeginInvokeOnMainThread(new NSAction(action));
#elif MONODROID
        Owner.RunOnUiThread(action);
#elif WP7
       Deployment.Current.Dispatcher.BeginInvoke(action);
#endif
      }
    }
  }
}


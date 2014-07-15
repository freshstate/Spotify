
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;

namespace Spotify.Core
{
  public abstract class ViewModelBaseExtended : ViewModelBase
  {
    protected override void RaisePropertyChanged(string propertyName)
    {
      //Dispatch event on the UI Thread
      ExtendedDispatcher.Dispatcher.BeginInvoke(() =>
        base.RaisePropertyChanged(propertyName)
       );
    }
  }


}


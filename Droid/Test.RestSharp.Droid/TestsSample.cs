using System;
using NUnit.Framework;
using RestSharp;

namespace Test.RestSharp.Droid
{
  [TestFixture]
  public class TestsSample
  {
    
    const string BaseUri = "https://api.spotify.com/";
    const string ClientID = "b0abdbcad3ca4947881242a102997fba";
    const string ClientSecret = "04236a53f2ff418cb9230afeeef05a05";


    [SetUp]
    public void Setup()
    {
    }

    [TearDown]
    public void Tear()
    {
    }

    [Test]
    public void SearchForMuseTrack()
    {
      var client = new RestClient();
      client.BaseUrl = BaseUri;

      var request = new RestRequest("/v1/search", Method.GET);
      request.AddParameter("client_id", ClientID);
      request.AddParameter("client_secret", ClientSecret);
      request.AddParameter("q", "muse");
      request.AddParameter("type", "artist");

      RestResponse response = (RestResponse)client.Execute(request);
      System.Console.WriteLine(response.Content);

    }
  }
}


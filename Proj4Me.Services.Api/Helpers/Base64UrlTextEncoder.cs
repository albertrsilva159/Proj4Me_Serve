using System;

namespace Proj4Me.Services.Api.Helpers
{
  public class Base64UrlTextEncoder
  {
    public string Encode(byte[] data)
    {
      if (data == null)
      {
        throw new ArgumentNullException("data");
      }

      return Convert.ToBase64String(data).TrimEnd('=').Replace('+', '-').Replace('/', '_');
    }

    public byte[] Decode(string text)
    {
      if (text == null)
      {
        throw new ArgumentNullException("text");
      }

      return Convert.FromBase64String(Pad(text.Replace('-', '+').Replace('_', '/')));
    }

    private static string Pad(string text)
    {
      var padding = 3 - ((text.Length + 3) % 4);
      if (padding == 0)
      {
        return text;
      }
      return text + new string('=', padding);
    }
  }
}

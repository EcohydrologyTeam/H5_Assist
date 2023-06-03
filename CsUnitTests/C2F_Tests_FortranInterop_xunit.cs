using Xunit;
using System;

namespace C2F_Tests
{
  [Trait("xunit: All", "")]
  [Trait("xunit: C2F All", "")]
  [Trait("xunit: C2F Fortran Interop", "")]
  public class C2F_Tests_FortranInterop_xunit
  {

    [Fact]
    public void C2F_Test_return_string()
    {
      string mystring = "";
      int str_len = 256;
      char[] char_mystring = new char[str_len];
      bool is_ok;

      is_ok = C2F_Interface.return_string(char_mystring, ref str_len);
      mystring = new string(char_mystring);
      Console.WriteLine(mystring);
    }

    [Fact]
    public void C2F_Test_return_string_cdecl()
    {
      int str_len = 4;
      char[,] char_mystring = new char[1, str_len];
      bool is_ok;

      is_ok = C2F_Interface.return_string_cdecl(char_mystring, ref str_len);
      //mystring = new string(char_mystring);
      //Console.WriteLine(mystring);
    }

    [Fact]
    public void C2F_Test_send_string_array_cdecl()
    {
      int str_len = 13;
      long npts = 2;
      char[,] char_array = new char[str_len, npts];
      bool is_ok;

      // Test Fortran function sending a string array to this function
      is_ok = C2F_Interface.send_string_array_cdecl(char_array, ref str_len, ref npts);
      string[] string_array = C2F_Utilities.CharArrayToStringArray(char_array, str_len, npts);

      for (int i = 0; i < npts; i++) {
        Console.WriteLine(string_array[i]);
      }
    }

    [Fact]
    public void C2F_Test_receive_string_array_cdecl()
    {
      int str_len = 13;
      long npts = 2;
      string[] string_array = new string[npts];
      string_array[0] = "abcdefghijklm";
      string_array[1] = "nopqrstuvwxyz";
      bool is_ok;

      char[,] char_array = C2F_Utilities.StringArrayToCharArray(string_array, str_len, npts);

      // Test Fortran function receiving string array from this function
      is_ok = C2F_Interface.receive_string_array_cdecl(char_array, ref str_len, ref npts);
    }
  }
}

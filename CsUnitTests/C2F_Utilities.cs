using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2F_Tests
{
  public class C2F_Utilities
  {
    public static bool H5OpenFileAndDataset(string infile, string group_name,
        string dataset_name, int file_access_flag, ref int file_id,
        ref int group_id, ref int dataset_id)
    // Open HDF5 file and then open a dataset
    // H5OpenFile intializes the HDF5 interface
    {
      bool is_ok = false;
      is_ok = C2F_Interface.h5_open_file(infile, ref file_access_flag, ref file_id, infile.Length);
      is_ok = C2F_Interface.h5_open_group(ref file_id, group_name, ref group_id, group_name.Length);
      if (!is_ok) return false;
      is_ok = C2F_Interface.h5_open_dataset(ref group_id, dataset_name, ref dataset_id, dataset_name.Length);
      if (!is_ok) return false;
      return is_ok;
    }

    public static bool H5CloseDatasetAndFile(int file_id, int group_id, int dataset_id)
    {
      // Close HDF dataset and file
      // H5CloseFile terminates the HDF5 interface
      bool is_ok = false;
      is_ok = C2F_Interface.h5_close_dataset(ref dataset_id);
      if (!is_ok) return false;
      is_ok = C2F_Interface.h5_close_group(ref group_id);
      if (!is_ok) return false;
      is_ok = H5CloseFile(file_id);
      if (!is_ok) return false;
      return is_ok;
    }

    public static bool H5CloseAttribute(int attribute_id)
    {
      bool is_ok = false;
      if (attribute_id != 0) {
        is_ok = C2F_Interface.h5_close_attribute(ref attribute_id);
      }
      return is_ok;
    }

    public static bool H5CloseDataset(int dataset_id)
    {
      bool is_ok = false;
      if (dataset_id != 0) {
        is_ok = C2F_Interface.h5_close_dataset(ref dataset_id);
      }
      return is_ok;
    }

    public static bool H5CloseGroup(int group_id)
    {
      bool is_ok = false;
      if (group_id != 0) {
        is_ok = C2F_Interface.h5_close_dataset(ref group_id);
      }
      return is_ok;
    }

    public static bool H5CloseFile(int file_id)
    {
      bool is_ok = false;
      if (file_id != 0) {
        is_ok = C2F_Interface.h5_close_dataset(ref file_id);
      }
      return is_ok;
    }

    public static string[] ToStringArray(StringBuilder SBstring, long npts, int str_len)
    {
      // Convert a StringBuilder object to a string array.
      // This supports handling Fortran routines that pass arrays of strings.
      // Calling the Fortran routine using a StringBuilder object passes the 
      // Fortran array of strings as a single string. This function parses
      // the StringBuilder object's string into an array of strings.
      string[] StringArray = new String[npts];
      int j = 0;
      for (int i = 0; i < npts; i++) {
        StringArray[i] = SBstring.ToString(j, str_len);
        j += str_len;
      }
      return StringArray;
    }

    // Convert character array returned by Fortran function, using the
    // Cdecl calling convention and bind(c), to a C# string array
    public static string[] CharArrayToStringArray(char[,] char_array, int str_len, long npts)
    {
      char[] ca = new char[str_len];
      string[] string_array = new string[npts];
      for (int j = 0; j < npts; j++) {
        for (int i = 0; i < str_len; i++) {
          ca[i] = char_array[i, j];
        }
        string_array[j] = new string(ca);
      }
      return string_array;
    }

    // Convert a C# string array to a character array to be sent to
    // a Fortran function, using the Cdecl calling convention and bind(c)
    public static char[,] StringArrayToCharArray(string[] string_array, int str_len, long npts)
    {
      char[,] char_array = new char[str_len, npts];
      char[] ca = new char[str_len];
      for (int j = 0; j < npts; j++) {
        ca = string_array[j].ToCharArray();
        for (int i = 0; i < str_len; i++) {
          char_array[i, j] = ca[i];
        }
      }
      return char_array;
    }


  }
}

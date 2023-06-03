!DEC$ FREEFORM
!* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
! HDF5 Interface Module: Basic Tests
!* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
!
! Written by Todd Steissberg, 2017
!
module h5_basictests
  !
  use, non_intrinsic  :: hdf5
  use, intrinsic      :: iso_c_binding ! provides: c_float, c_int, c_ptr
  use, non_intrinsic  :: h5_globals
  !
  implicit none
  !
  contains
  !
  ! ----- Basic Tests -----
  !
  ! .....................................................................................................
  !
  ! Provide a simple test: add one to the input argument
  ! .....................................................................................................
  integer function add_one(x) result(sum)
    !DEC$ IF DEFINED (_WIN32)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"add_one" :: add_one
    !DEC$ END IF
    !
    integer, intent(in) :: x
    sum = x + 1
    return
  end function
  !
  ! .....................................................................................................
  ! Test returning passing a single string, for .Net interoperability - STDCALL Convention
  ! .....................................................................................................
  logical function return_string(mystring, str_len) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"return_string" :: return_string
    !
    ! Note: In .Net, use a char[] array to pass the string
    ! You may also use a byte[] array or a StringBuilder object (need to test this)
    integer, intent(in)                 :: str_len  ! Length of string to return
    character(len=str_len), &
      intent(out)                       :: mystring ! String to return
    mystring = "abcdefghijklm"
    success = .true.
  end function
  !
  ! .....................................................................................................
  ! Test returning passing a single string, for .Net interoperability - Cdecl & bind(c) Convention
  ! .....................................................................................................
  logical function return_string_cdecl(char_array, str_len) result(success) bind(c, name="return_string_cdecl")
    !DEC$ ATTRIBUTES DLLEXPORT :: return_string_cdecl
    !
    ! Note: In .Net, use a char[] array to pass the string
    ! You may also use a byte[] array or a StringBuilder object (need to test this)
    integer, intent(in)                 :: str_len  ! Length of strings string arrays
    character, &
      dimension(str_len), &
      intent(inout)                     :: char_array ! String to return
    char_array(1) = "a"
    char_array(2) = "b"
    char_array(3) = "c"
    success = .true.
  end function
  !
  ! .....................................................................................................
  !
  ! Test returning passing an array of strings, for .Net interoperability
  ! .....................................................................................................
  logical function return_string_array(mystrings, str_len, npts) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"return_string_array" :: return_string_array
    !
    ! Note: In .Net, use a StringBuilder object to pass the array of strings
    ! C# Example:
    ! var SB = new StringBuilder(npts * str_len)
    ! SB = H5Interface.return_string_array(SB, ref str_len, ref npts)
    ! Then parse SB to a string[] array in C#.
    integer, intent(in)                 :: str_len  ! Length of strings in array
    integer, intent(in)                 :: npts     ! Number of strings in array
    character(len=str_len), &
      dimension(npts), &
      intent(inout)                     :: mystrings ! String array to output (should be allocated by caller)
    mystrings(1) = "abcdefghijklm"
    mystrings(2) = "nopqrstuvwxyz"
    success = .true.
  end function
  !
  ! .....................................................................................................
  ! Test sending a string array, for .Net interoperability - Cdecl & bind(c) Convention
  ! .....................................................................................................
  logical function send_string_array_cdecl(char_array, str_len, npts) result(success) bind(c, name="send_string_array_cdecl")
    !DEC$ ATTRIBUTES DLLEXPORT :: send_string_array_cdecl
    !
    ! Note: In .Net, use a char[] array to pass the string
    ! You may also use a byte[] array or a StringBuilder object (need to test this)
    integer, intent(in)                 :: str_len
    integer, intent(in)                 :: npts
    !
    character, &
      dimension(npts, str_len), &
      intent(inout)                     :: char_array     ! Character array to receive
    character(len=str_len), & 
      dimension(npts)                   :: string_array   ! String array to print below
    integer                             :: i, j
    !
    ! Populate string array
    string_array(1) = "abcdefghijklm"
    string_array(2) = "nopqrstuvwxyz"
    !
    ! Transfer strings to character array for output
    do j = 1, str_len
      do i = 1, npts
        char_array(i,j) = string_array(i)(j:j)
      end do
    end do
    !
    success = .true.
  end function
  !
  ! .....................................................................................................
  ! Test receiving a string array, for .Net interoperability - Cdecl & bind(c) Convention
  ! .....................................................................................................
  logical function receive_string_array_cdecl(char_array, str_len, npts) result(success) bind(c, name="receive_string_array_cdecl")
    !DEC$ ATTRIBUTES DLLEXPORT :: receive_string_array_cdecl
    !
    ! Note: In .Net, use a char[] array to pass the string
    ! You may also use a byte[] array or a StringBuilder object (need to test this)
    integer, intent(in)                 :: str_len
    integer, intent(in)                 :: npts
    !
    character, &
      dimension(npts, str_len), &
      intent(inout)                     :: char_array     ! Character array to receive
    character(len=str_len), & 
      dimension(npts)                   :: string_array   ! String array to print below
    integer                             :: i, j
    integer                             :: logfile = 106
    !
    ! Transfer character array to string array for printing below
    do j = 1, str_len
      do i = 1, npts
        string_array(i)(j:j) = char_array(i,j)
      end do
    end do
    !
    ! Print the strings received
    if (DEBUG) open(logfile, file = "receive_string_array_cdecl.log.txt")
    if (DEBUG) write(logfile, *) string_array(1)
    if (DEBUG) write(logfile, *) string_array(2)
    if (DEBUG) close(logfile)
    !
    success = .true.
  end function
  !
  ! .....................................................................................................
  !
  ! Pass 2D float array
  ! .....................................................................................................
  logical function pass_2d_float_array_two_way(myarray, nrows, ncols) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"pass_2d_float_array_two_way" :: pass_2d_float_array_two_way
    !
    real(kind=c_float), &
      dimension(ncols, nrows), &
      intent(inout)                     :: myarray
    integer, intent(in)                 :: nrows
    integer, intent(in)                 :: ncols
    integer                             :: i, j
    !
    success = .false.
    do j = 1, nrows
      do i = 1, nrows
        myarray(i, j) = i * 100 + j
      end do
    end do
    !
    success = .true.
  end function
  !
  ! .....................................................................................................
  !
  ! Pass 2D integer array
  ! .....................................................................................................
  logical function pass_2d_int_array_two_way(myarray, nrows, ncols) result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"pass_2d_int_array_two_way" :: pass_2d_int_array_two_way
    !
    integer, dimension(ncols, nrows), &
      intent(inout)                     :: myarray
    integer, intent(in)                 :: nrows
    integer, intent(in)                 :: ncols
    integer                             :: i, j
    !
    success = .false.
    do j = 1, nrows
      do i = 1, nrows
        myarray(i, j) = i * 100 + j
      end do
    end do
    !
    success = .true.
  end function
  !
end module
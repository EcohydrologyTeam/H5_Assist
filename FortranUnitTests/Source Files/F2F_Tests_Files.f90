!DEC$ FREEFORM
!* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
! HDF5 Unit Tests - File Tests
!* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
!
! Created by Todd Steissberg, 2017
!
!------------------------------------------------------------------------------------------------
! See notes in F2F_Tests_Globals.f90
!------------------------------------------------------------------------------------------------
!
module F2F_Tests_Files
  !
  use, non_intrinsic  :: hdf5
  use, non_intrinsic  :: h5_globals
  use, non_intrinsic  :: h5_utilities
  use, non_intrinsic  :: h5_files
  use :: F2F_Tests_Globals
  !
  implicit none
  !
  contains
  !
  ! ...............................................................................................
  !
  ! Test opening and closing an HDF5 file
  ! ...............................................................................................
  logical function test_h5_open_close_file() result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_open_close_file" :: test_h5_open_close_file
    logical :: is_ok
    integer :: file_access_flag = READ_ONLY
    integer :: file_id = 0
    character(len=STD_STR_LEN) :: infile
    !
    success = .false.
    !
    infile = HDF_INFILE
    !
    is_ok = h5_init()
    if (.not. is_ok) return
    !
    is_ok = h5_open_file(infile, file_access_flag, file_id)
    if (.not. is_ok) return
    !
    is_ok = h5_close_file(file_id)
    if (.not. is_ok) return
    !
    is_ok = h5_terminate()
    if (.not. is_ok) return
    !
    success = .true.
  end function
  !
  ! ...............................................................................................
  !
  ! Test opening and closing an HDF5 file
  ! ...............................................................................................
  logical function test_h5_create_file() result(success)
    !DEC$ ATTRIBUTES STDCALL, REFERENCE, DLLEXPORT, ALIAS:"test_h5_create_file" :: test_h5_create_file
    logical :: is_ok
    integer :: file_id = 0
    character(len=STD_STR_LEN) :: filename
    !
    success = .false.
    !
    filename = "MyNewHDFfile_F2F_UnitTest.h5";
    !
    is_ok = h5_init()
    if (.not. is_ok) return
    !
    is_ok = h5_create_file(filename, TRUNCATE, file_id)
    !
    is_ok = h5_terminate()
    if (.not. is_ok) return
    !
    success = .true.
  end function

end module
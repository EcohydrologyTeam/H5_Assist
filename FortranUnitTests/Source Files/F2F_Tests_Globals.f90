!DEC$ FREEFORM
!* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
! HDF5 Unit Tests - Globals Tests
!* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
!
! Created by Todd Steissberg, 2017
!
module F2F_Tests_Globals
  !
  use, non_intrinsic :: hdf5
  use, non_intrinsic :: h5_globals
  !
  implicit none
  !
  !----------------------------------------------------------------------------------
  ! This module establishes the global variables for the various test modules to
  ! test the HDF5 functions in h5_interface.f90.
  ! The following information and notes cover the routines in the other test 
  ! modules.
  ! 
  !
  ! Each unit test module is to be driven by the C# class having the same 
  ! name. The naming convention for each Fortran test module starts with F2F,
  ! which denotes Fortran-to-Fortran testing.

  ! These Fortran tests depending on h5_interface.f90, which depends on the 
  ! standard HDF5 modules. 
  
  ! The current version has only been tested on 64-bit Windows 2007 systems using 
  ! the 64-bit HDF5 libraries.
  !
  ! The C# unit tests in this Visual Studio Solution are located in the classes 
  ! named F2F*.cs. The C# unit tests are organized into several classes named
  ! F2F*.cs. 
  !
  ! The DLL import declarations for this module are located in F2F_Interface.cs.
  ! 
  ! Each C# test function consists of only one line. That line of code calls the 
  ! corresponding function in this module. The unit test in this module then sets 
  ! up and calls one or more functions in h5_interface.f90. Each of those functions 
  ! calls one or more subroutines in the The standard HDF5 Fortran library. Those 
  ! routines are actually, in turn, wrappers for the standard HDF5 routines written 
  ! in C.
  !
  ! Notes:
  !   1. The trim() function is not used in the unit tests. The routines in 
  !      h5_interface.h5 were intended to do all string trimming and other major 
  !      pre-processing to reduce the likelihood of errors and to improve ease of 
  !      use.
  !
  ! HDF5 file access flags (these are defined in h5_interface.f90, which is imported above)
  !integer, parameter  :: READ_ONLY             = 0 ! HDF5 constant: H5F_ACC_RDONLY_F
  !                                                 ! Existing file is opened with read-only access. 
  !                                                 ! If file does not exist, H5Fopen fails.
  !integer, parameter  :: READ_WRITE            = 1 ! HDF5 constant: H5F_ACC_RDWR_F
  !                                                 ! Existing file is opened with read-write access. 
  !                                                 ! If file does not exist, H5Fopen fails.
  !integer, parameter  :: TRUNCATE              = 2 ! HDF5 constant: H5F_ACC_TRUN_F
  !                                                 ! File is truncated upon opening, i.e., if file 
  !                                                 ! already exists, file is opened with read-write 
  !                                                 ! access and new data overwrites existing data, 
  !                                                 ! destroying all prior content. If file does not exist, 
  !                                                 ! it is created and opened with read-write access.
  !integer, parameter  :: CREATE_AND_READ_WRITE = 4 ! HDF5 constant: H5F_ACC_EXCL_F
  !                                                 ! If file already exists, H5Fcreate fails. If file 
  !                                                 ! does not exist, it is created and opened with 
  !                                                 ! read-write access.
  ! 
  ! HDF5 Data Types:
  !integer, parameter        :: hid_t = 4   ! Manage references to nodes(ID), unsigned integer
  !integer, parameter        :: hsize_t = 8 ! Native multi-precision integer
  !integer, parameter        :: size_t = 8  ! C native unsigned integer
  !
  ! Fortran native types in HDF5
  integer(kind=4), parameter  :: TEST_H5T_NATIVE_INTEGER    = 50331741 ! Native integer type
  integer(kind=4), parameter  :: TEST_H5T_NATIVE_REAL       = 50331742 ! Single precision real type
  integer(kind=4), parameter  :: TEST_H5T_NATIVE_DOUBLE     = 50331743 ! Double precision real type
  integer(kind=4), parameter  :: TEST_H5T_NATIVE_CHARACTER  = 50331744 ! Character
  integer(kind=4), parameter  :: TEST_H5T_FORTRAN_S1        = 50331786 ! Fortran string type
  !
  ! Set HDF5 test input filename
  character(len=128)  :: HDF_INFILE = "LMNRRAS.p03.hdf"
  character(len=128)  :: HDF_INFILE_COPY2 = "LMNRRAS.p03.copy2.hdf"
  !
  ! Standard string length for this module for testing
  integer, parameter  :: STD_STR_LEN = 256 
  !
  contains
  !
  ! ...............................................................................................
  !
  ! Compare two floating point values: Real type
  ! ...............................................................................................
  logical function are_equal_r(expected, actual, tolerance) result(success)
    ! Return true only if their absolute difference is less than a specified tolerance
    real(kind=C_FLOAT), intent(in)      :: expected   ! Expected value
    real(kind=C_FLOAT), intent(in)      :: actual     ! Actual value
    real(kind=C_FLOAT), intent(in)      :: tolerance  ! Tolerance (fractional value)
    real(kind=C_FLOAT)                  :: diff
    !
    success = .false.
    !
    diff = abs(actual - expected)
    if (diff > tolerance) return
    !
    success = .true.
  end function
  !
  ! ...............................................................................................
  !
  ! Compare two values: Integer type
  ! ...............................................................................................
  logical function are_equal_i(expected, actual) result(success)
    ! Return true only if their absolute difference is less than a specified tolerance
    integer(kind=C_INT), intent(in)      :: expected   ! Expected value
    integer(kind=C_INT), intent(in)      :: actual     ! Actual value
    integer(kind=C_INT)                  :: diff
    !
    success = .false.
    !
    diff = abs(actual - expected)
    if (diff > 0) return
    !
    success = .true.
  end function
  !
  ! ...............................................................................................
  !
  ! Compare two values: String type
  ! ...............................................................................................
  logical function are_equal_c(expected, actual) result(success)
    ! Return true only if the trimmed strings are the same
    character(kind=C_CHAR, len=*), &
      intent(in)                        :: expected   ! Expected value
    character(kind=C_CHAR, len=*), &
      intent(in)                        :: actual     ! Actual value
    !
    success = .false.
    !
    if (len_trim(actual) /= len_trim(expected)) return
    !
    success = .true.
  end function

end module
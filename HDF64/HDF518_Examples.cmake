cmake_minimum_required(VERSION 3.1.0 FATAL_ERROR)
###############################################################################################################
# This script will build and run the examples from a folder
# Execute from a command line:
#     ctest -S HDF518_Examples.cmake,OPTION=VALUE -C Release -VV -O test.log
###############################################################################################################

set(CTEST_CMAKE_GENERATOR "Visual Studio 12 2013 Win64")
set(CTEST_DASHBOARD_ROOT ${CTEST_SCRIPT_DIRECTORY})

# handle input parameters to script.
#INSTALLDIR - HDF5-1.8 root folder
#CTEST_BUILD_CONFIGURATION - Release, Debug, RelWithDebInfo
#CTEST_SOURCE_NAME - name of source folder; HDF4Examples
#STATIC_LIBRARIES - Default is YES
#FORTRAN_LIBRARIES - Default is NO
##NO_MAC_FORTRAN - set to TRUE to allow shared libs on a Mac)
if(DEFINED CTEST_SCRIPT_ARG)
    # transform ctest script arguments of the form
    # script.ctest,var1=value1,var2=value2
    # to variables with the respective names set to the respective values
    string(REPLACE "," ";" script_args "${CTEST_SCRIPT_ARG}")
    foreach(current_var ${script_args})
        if ("${current_var}" MATCHES "^([^=]+)=(.+)$")
            set("${CMAKE_MATCH_1}" "${CMAKE_MATCH_2}")
        endif()
    endforeach()
endif()
if(NOT DEFINED INSTALLDIR)
    set(INSTALLDIR "C:/Program Files/HDF_Group/HDF5/1.8.17")
endif()
if(NOT DEFINED CTEST_BUILD_CONFIGURATION)
    set(CTEST_BUILD_CONFIGURATION "Release")
endif()
if(NOT DEFINED CTEST_SOURCE_NAME)
    set(CTEST_SOURCE_NAME "HDF5Examples")
endif()
if(NOT DEFINED STATIC_LIBRARIES)
    set(STATICLIBRARIES "YES")
else(NOT DEFINED STATIC_LIBRARIES)
    set(STATICLIBRARIES "NO")
endif()
if(NOT DEFINED FORTRAN_LIBRARIES)
    set(FORTRANLIBRARIES "NO")
else(NOT DEFINED FORTRAN_LIBRARIES)
    set(FORTRANLIBRARIES "YES")
endif()

#TAR_SOURCE - name of tarfile
#if(NOT DEFINED TAR_SOURCE)
#  set(CTEST_USE_TAR_SOURCE "HDF5Examples-1.2.1-Source")
#endif()

###############################################################################################################
#     Adjust the following SET Commands as needed
###############################################################################################################
if(WIN32)
  if(${STATICLIBRARIES})
    set(BUILD_OPTIONS "${BUILD_OPTIONS} -DBUILD_SHARED_LIBS:BOOL=OFF")
  endif()
  set(ENV{HDF5_DIR} "${INSTALLDIR}/cmake")
  set(CTEST_BINARY_NAME ${CTEST_SOURCE_NAME}\\build)
  set(CTEST_SOURCE_DIRECTORY "${CTEST_DASHBOARD_ROOT}\\${CTEST_SOURCE_NAME}")
  set(CTEST_BINARY_DIRECTORY "${CTEST_DASHBOARD_ROOT}\\${CTEST_BINARY_NAME}")
else(WIN32)
  if(${STATICLIBRARIES})
    set(BUILD_OPTIONS "${BUILD_OPTIONS} -DBUILD_SHARED_LIBS:BOOL=OFF -DCMAKE_ANSI_CFLAGS:STRING=-fPIC")
  endif()
  set(ENV{HDF5_DIR} "${INSTALLDIR}/share/cmake")
  set(ENV{LD_LIBRARY_PATH} "${INSTALLDIR}/lib")
  set(CTEST_BINARY_NAME ${CTEST_SOURCE_NAME}/build)
  set(CTEST_SOURCE_DIRECTORY "${CTEST_DASHBOARD_ROOT}/${CTEST_SOURCE_NAME}")
  set(CTEST_BINARY_DIRECTORY "${CTEST_DASHBOARD_ROOT}/${CTEST_BINARY_NAME}")
endif(WIN32)
if(${FORTRANLIBRARIES})
  set(ADD_BUILD_OPTIONS "${ADD_BUILD_OPTIONS} -DHDF_BUILD_FORTRAN:BOOL=ON")
else()
  set(ADD_BUILD_OPTIONS "${ADD_BUILD_OPTIONS} -DHDF_BUILD_FORTRAN:BOOL=OFF")
endif()
set(BUILD_OPTIONS "${BUILD_OPTIONS} -DHDF5_PACKAGE_NAME:STRING=hdf5")

###############################################################################################################
# For any comments please contact cdashhelp@hdfgroup.org
#
###############################################################################################################

#-----------------------------------------------------------------------------
# MAC machines need special option
#-----------------------------------------------------------------------------
if(APPLE)
  # Compiler choice
  execute_process(COMMAND xcrun --find cc OUTPUT_VARIABLE XCODE_CC OUTPUT_STRIP_TRAILING_WHITESPACE)
  execute_process(COMMAND xcrun --find c++ OUTPUT_VARIABLE XCODE_CXX OUTPUT_STRIP_TRAILING_WHITESPACE)
  set(ENV{CC} "${XCODE_CC}")
  set(ENV{CXX} "${XCODE_CXX}")
  if(NOT NO_MAC_FORTRAN)
    # Shared fortran is not supported, build static
    set(BUILD_OPTIONS "${BUILD_OPTIONS} -DBUILD_SHARED_LIBS:BOOL=OFF -DCMAKE_ANSI_CFLAGS:STRING=-fPIC")
  else()
    set(BUILD_OPTIONS "${BUILD_OPTIONS} -DHDF_BUILD_FORTRAN:BOOL=OFF")
  endif()
  set(BUILD_OPTIONS "${BUILD_OPTIONS} -DCTEST_USE_LAUNCHERS:BOOL=ON -DCMAKE_BUILD_WITH_INSTALL_RPATH:BOOL=OFF")
endif()

#-----------------------------------------------------------------------------
set(CTEST_CMAKE_COMMAND "\"${CMAKE_COMMAND}\"")
## --------------------------
if(CTEST_USE_TAR_SOURCE)
  ## Uncompress source if tar or zip file provided
  ## --------------------------
  if(WIN32)
    message(STATUS "extracting... [${CMAKE_EXECUTABLE_NAME} -E tar -xvf ${CTEST_USE_TAR_SOURCE}.zip]")
    execute_process(COMMAND ${CMAKE_EXECUTABLE_NAME} -E tar -xvf ${CTEST_DASHBOARD_ROOT}\\${CTEST_USE_TAR_SOURCE}.zip RESULT_VARIABLE rv)
  else()
    message(STATUS "extracting... [${CMAKE_EXECUTABLE_NAME} -E tar -xvf ${CTEST_USE_TAR_SOURCE}.tar]")
    execute_process(COMMAND ${CMAKE_EXECUTABLE_NAME} -E tar -xvf ${CTEST_DASHBOARD_ROOT}/${CTEST_USE_TAR_SOURCE}.tar RESULT_VARIABLE rv)
  endif()

  if(NOT rv EQUAL 0)
    message(STATUS "extracting... [error-(${rv}) clean up]")
    file(REMOVE_RECURSE "${CTEST_SOURCE_DIRECTORY}")
    message(FATAL_ERROR "error: extract of ${CTEST_SOURCE_NAME} failed")
  endif()
endif(CTEST_USE_TAR_SOURCE)

#-----------------------------------------------------------------------------
## Clear the build directory
## --------------------------
set(CTEST_START_WITH_EMPTY_BINARY_DIRECTORY TRUE)
if (EXISTS "${CTEST_BINARY_DIRECTORY}" AND IS_DIRECTORY "${CTEST_BINARY_DIRECTORY}")
  ctest_empty_binary_directory(${CTEST_BINARY_DIRECTORY})
else ()
  file(MAKE_DIRECTORY "${CTEST_BINARY_DIRECTORY}")
endif ()

# Use multiple CPU cores to build
include(ProcessorCount)
ProcessorCount(N)
if(NOT N EQUAL 0)
  if(NOT WIN32)
    set(CTEST_BUILD_FLAGS -j${N})
  endif()
  set(ctest_test_args ${ctest_test_args} PARALLEL_LEVEL ${N})
endif()
set (CTEST_CONFIGURE_COMMAND
    "${CTEST_CMAKE_COMMAND} -C \"${CTEST_SOURCE_DIRECTORY}/config/cmake/cacheinit.cmake\" -DCMAKE_BUILD_TYPE:STRING=${CTEST_BUILD_CONFIGURATION} ${BUILD_OPTIONS} \"-G${CTEST_CMAKE_GENERATOR}\" \"${CTEST_SOURCE_DIRECTORY}\""
)

#-----------------------------------------------------------------------------
## -- set output to english
set($ENV{LC_MESSAGES}  "en_EN")

#-----------------------------------------------------------------------------
configure_file(${CTEST_SOURCE_DIRECTORY}/config/cmake/CTestCustom.cmake ${CTEST_BINARY_DIRECTORY}/CTestCustom.cmake)
ctest_read_custom_files ("${CTEST_BINARY_DIRECTORY}")
## NORMAL process
## --------------------------
ctest_start (Experimental)
ctest_configure (BUILD "${CTEST_BINARY_DIRECTORY}")
if(LOCAL_SUBMIT)
  ctest_submit (PARTS Configure Notes)
endif()
ctest_build (BUILD "${CTEST_BINARY_DIRECTORY}" APPEND)
if(LOCAL_SUBMIT)
  ctest_submit (PARTS Build)
endif()
ctest_test (BUILD "${CTEST_BINARY_DIRECTORY}" APPEND ${ctest_test_args} RETURN_VALUE res)
if(LOCAL_SUBMIT)
  ctest_submit (PARTS Test)
endif()
if(res GREATER 0)
  message (FATAL_ERROR "tests FAILED")
endif()
#-----------------------------------------------------------------------------
##############################################################################################################
message(STATUS "DONE")

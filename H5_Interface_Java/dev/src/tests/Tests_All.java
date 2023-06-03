package tests;

import org.junit.runner.RunWith;
import org.junit.runners.Suite;

// Specify a runner class: Suite.class
@RunWith(Suite.class)

// Specify an array of test classes
@Suite.SuiteClasses(
        {
                Test_H5File.class,
                Test_H5Group.class,
                Test_H5Dataset.class,
                Test_H5Attribute.class
        }
)

// the actual class is empty
public class Tests_All
{
}
